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
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public GameStateMachine Bind(Level level)
        {
            _level = level;
            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            
            _container.Bind<IPauseService>().To<PauseService>().FromNew().AsSingle().NonLazy();
            BindModels();
            BindInput();
            BindMap();
            BindFactories();
            BindServices();
            
            return BindGameStateMachine();
        }

        private GameStateMachine BindGameStateMachine()
        {
            var spawnerService = _container.Resolve<IWaveSpawnerService>();
            var pauseService = _container.Resolve<IPauseService>();
            var gameStateService = _container.Resolve<IGameStateService>();
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            
            GameStateMachine gameStateMachine = new GameStateMachine(spawnerService, pauseService, playerHealthProxy, gameStateService);

            return gameStateMachine;
        }

        private void BindModels()
        {
            var health = new PlayerHealth(_level.LevelConfig.InitialHealth);
            var playerHealthProxy = new PlayerHealthProxy(health);
            
            var buildingCurrency = new PlayerBuildingCurrency(_level.LevelConfig.InitialBuildingCurrency);
            var buildingCurrencyProxy = new PlayerBuildingCurrencyProxy(buildingCurrency);
            
            _container.Bind<PlayerHealthProxy>().FromInstance(playerHealthProxy).AsSingle().NonLazy();
            _container.Bind<PlayerBuildingCurrencyProxy>().FromInstance(buildingCurrencyProxy).AsSingle().NonLazy();
        }

        private void BindInput()
        {
            _inputHandler = new InputHandler();
            _container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(_inputHandler).AsSingle().NonLazy();
        }

        private void BindMap()
        {
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            
            var mapCoordinator = new MapCoordinator(_level.BaseTileMap, _level.EnvironmentTileMap, _level.LevelConfig.TileSetConfig, _inputHandler);
            _container.Bind<MapCoordinator>().FromInstance(mapCoordinator).AsSingle().NonLazy();
            
            _level.EnemyFinish.Init(playerHealthProxy);
        }

        private void BindFactories()
        {
            var prefabReferences = _container.Resolve<PrefabReferencesConfig>();
            var pauseService = _container.Resolve<IPauseService>();
            var mapCoordinator = _container.Resolve<MapCoordinator>();
            
            var towerFactory = new TowerFactory(
                _container, 
                _level.LevelConfig.TowerGenerationConfigs,
                prefabReferences,
                mapCoordinator,
                _inputHandler,
                _level.TowersParent,
                _monoBehaviourWrapper,
                pauseService);
            
            _container.Bind<TowerFactory>().FromInstance(towerFactory).AsSingle().NonLazy();
            
            var enemyFactory = new EnemyFactory(_container, prefabReferences.EnemyPrefab, _level.EnemiesParent);
            _container.Bind<EnemyFactory>().FromInstance(enemyFactory).AsSingle().NonLazy();
            
            MergeHandler.Init(towerFactory);
        }

        private void BindServices()
        {
            bindWaveSpawnerService();
            
            _container.Bind<IGameStateService>().To<GameStateService>().FromNew().AsSingle().NonLazy();
            _container.Bind<IScoreService>().To<ScoreService>().FromNew().AsSingle().NonLazy();
            _container.Bind<IRewardCalculatorService>().To<RewardCalculatorService>().FromNew().AsSingle().NonLazy();
            
            void bindWaveSpawnerService()
            {
                var pauseService = _container.Resolve<IPauseService>();
                var enemyFactory = _container.Resolve<EnemyFactory>();
                
                
                List<Vector3> path = _level.PathPoints.Select(x => x.position).ToList();
            
                WaveSpawnerService spawnerService = new WaveSpawnerService(enemyFactory, _level.LevelConfig.Waves, path, _level.EnemySpawnPosition.position, _monoBehaviourWrapper);
                _container.Bind<IWaveSpawnerService>().To<WaveSpawnerService>().FromInstance(spawnerService).AsSingle().NonLazy();
                pauseService.Register(spawnerService);
            }
        }
    }
}