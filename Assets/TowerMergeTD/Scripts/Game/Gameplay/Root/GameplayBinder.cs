using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayBinder
    {
        private readonly DiContainer _container;

        private LevelConfig _levelConfig;
        private Tilemap _baseTileMap;
        private Tilemap _environmentTileMap;
        private TileSetConfig _tileSetConfig;
        private InputHandler _inputHandler;
        private Transform _towersParent;
        private Transform _enemyParent;
        private Transform[] _pathPoints;
        private Vector2 _enemySpawnPosition;
        private EnemyFinishTrigger _enemyFinish;
        
        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private PlayerHealthProxy _playerHealthProxy;
        private MapCoordinator _mapCoordinator;
        private EnemyFactory _enemyFactory;
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public void Bind(
            LevelConfig levelConfig, 
            Tilemap baseTileMap, 
            Tilemap environmentTileMap, 
            TileSetConfig tileSetConfig, 
            Transform towersParent, 
            Transform enemyParent,
            Transform[] pathPoints,
            Vector2 enemySpawnPosition,
            EnemyFinishTrigger enemyFinish)
        {
            _levelConfig = levelConfig;
            _baseTileMap = baseTileMap;
            _environmentTileMap = environmentTileMap;
            _tileSetConfig = tileSetConfig;
            _towersParent = towersParent;
            _enemyParent = enemyParent;
            _pathPoints = pathPoints;
            _enemySpawnPosition = enemySpawnPosition;
            _enemyFinish = enemyFinish;
            
            BindModels();
            BindInput();
            BindMap();
            BindFactories();
            BindServices();
        }

        private void BindModels()
        {
            PlayerHealth health = new PlayerHealth(_levelConfig.InitialHealth);
            _playerHealthProxy = new PlayerHealthProxy(health);
            _container.Bind<PlayerHealthProxy>().FromInstance(_playerHealthProxy).AsSingle().NonLazy();
            
            PlayerMoney playerMoney = new PlayerMoney(_levelConfig.InitialMoney);
            var playerMoneyProxy = new PlayerMoneyProxy(playerMoney);
            _container.Bind<PlayerMoneyProxy>().FromInstance(playerMoneyProxy).AsSingle().NonLazy();
        }

        private void BindInput()
        {
            _inputHandler = new InputHandler();
            _container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(_inputHandler).AsSingle().NonLazy();
        }

        private void BindMap()
        {
            _mapCoordinator = new MapCoordinator(_baseTileMap, _environmentTileMap, _tileSetConfig, _inputHandler);
            _container.Bind<MapCoordinator>().FromInstance(_mapCoordinator).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            var prefabReferences = _container.Resolve<PrefabReferencesConfig>();
            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            
            var towerFactory = new TowerFactory(
                _container, 
                _levelConfig.TowerGenerationConfigs,
                prefabReferences,
                _mapCoordinator,
                _inputHandler,
                _towersParent,
                _monoBehaviourWrapper);
            _container.Bind<TowerFactory>().FromInstance(towerFactory).AsSingle().NonLazy();
            
            _enemyFactory = new EnemyFactory(_container, prefabReferences.EnemyPrefab, _enemyParent);
            _container.Bind<EnemyFactory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
            
            MergeHandler.Init(towerFactory);
        }

        private void BindServices()
        {
            List<Vector3> path = _pathPoints.Select(x => x.position).ToList();
            
            WaveSpawnerService spawnerService = new WaveSpawnerService(_enemyFactory, _levelConfig.Waves, path, _enemySpawnPosition, _monoBehaviourWrapper);
            _container.Bind<IWaveSpawnerService>().To<WaveSpawnerService>().FromInstance(spawnerService).AsSingle().NonLazy();

            _enemyFinish.Init(_playerHealthProxy);
        }
    }
}