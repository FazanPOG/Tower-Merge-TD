using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayBinder
    {
        private readonly DiContainer _container;
        
        private Level _level;
        private InputHandler _inputHandler;
        
        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private PlayerHealthProxy _playerHealthProxy;
        private MapCoordinator _mapCoordinator;
        private EnemyFactory _enemyFactory;
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public void Bind(Level level)
        {
            _level = level;
            
            _container.Bind<IPauseService>().To<PauseService>().FromNew().AsSingle().NonLazy();
            BindModels();
            BindInput();
            BindMap();
            BindFactories();
            BindServices();

            _container.Bind<GameStateMachine>().FromNew().AsSingle().NonLazy();
        }

        private void BindModels()
        {
            PlayerHealth health = new PlayerHealth(_level.LevelConfig.InitialHealth);
            _playerHealthProxy = new PlayerHealthProxy(health);
            _container.Bind<PlayerHealthProxy>().FromInstance(_playerHealthProxy).AsSingle().NonLazy();
            
            PlayerMoney playerMoney = new PlayerMoney(_level.LevelConfig.InitialMoney);
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
            _mapCoordinator = new MapCoordinator(_level.BaseTileMap, _level.EnvironmentTileMap, _level.LevelConfig.TileSetConfig, _inputHandler);
            _container.Bind<MapCoordinator>().FromInstance(_mapCoordinator).AsSingle().NonLazy();
            
            _level.EnemyFinish.Init(_playerHealthProxy);
        }

        private void BindFactories()
        {
            var prefabReferences = _container.Resolve<PrefabReferencesConfig>();
            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            var pauseService = _container.Resolve<IPauseService>();
            
            var towerFactory = new TowerFactory(
                _container, 
                _level.LevelConfig.TowerGenerationConfigs,
                prefabReferences,
                _mapCoordinator,
                _inputHandler,
                _level.TowersParent,
                _monoBehaviourWrapper,
                pauseService);
            
            _container.Bind<TowerFactory>().FromInstance(towerFactory).AsSingle().NonLazy();
            
            _enemyFactory = new EnemyFactory(_container, prefabReferences.EnemyPrefab, _level.EnemiesParent);
            _container.Bind<EnemyFactory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
            
            MergeHandler.Init(towerFactory);
        }

        private void BindServices()
        {
            bindWaveSpawnerService();
            
            void bindWaveSpawnerService()
            {
                var pauseService = _container.Resolve<IPauseService>();
                List<Vector3> path = _level.PathPoints.Select(x => x.position).ToList();
            
                WaveSpawnerService spawnerService = new WaveSpawnerService(_enemyFactory, _level.LevelConfig.Waves, path, _level.EnemySpawnPosition.position, _monoBehaviourWrapper);
                _container.Bind<IWaveSpawnerService>().To<WaveSpawnerService>().FromInstance(spawnerService).AsSingle().NonLazy();
                pauseService.Register(spawnerService);
            }
        }
    }
}