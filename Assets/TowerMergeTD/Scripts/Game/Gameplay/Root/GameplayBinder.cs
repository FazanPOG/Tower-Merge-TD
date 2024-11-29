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
        private int _currentLevelIndex;

        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private Camera _mainCamera;
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public GameStateMachine Bind(Level currentLevel, int currentLevelIndex)
        {
            _level = currentLevel;
            _currentLevelIndex = currentLevelIndex;

            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            _mainCamera = Camera.main;
            
            _container.Bind<IPauseService>().To<PauseService>().FromNew().AsSingle().NonLazy();
            BindModels();
            BindInput();
            BindMap();
            BindFactories();
            BindServices();
            BindCameraSystem();

            return BindGameStateMachine();
        }

        private GameStateMachine BindGameStateMachine()
        {
            var mapCoordinator = _container.Resolve<MapCoordinator>();
            
            ITutorialBinder tutorialBinder;
            switch (_currentLevelIndex + 1)
            {
                case 1:
                    tutorialBinder = new Level1TutorialBinder(_monoBehaviourWrapper, mapCoordinator);
                    break;
                
                default:
                    tutorialBinder = null;
                    break;
            }
            
            GameStateMachine gameStateMachine = new GameStateMachine(_container, _level.LevelConfig, _currentLevelIndex, tutorialBinder);

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
            //TODO: change input depending on device
            _container.BindInterfacesTo<DesktopInput>().FromNew().AsSingle().WithArguments(_mainCamera).NonLazy();
        }

        private void BindMap()
        {
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            
            var mapCoordinator = new MapCoordinator(_level.BaseTileMap, _level.EnvironmentTileMap, _level.LevelConfig.TileSetConfig);
            _container.Bind<MapCoordinator>().FromInstance(mapCoordinator).AsSingle().NonLazy();

            foreach (var finish in _level.EnemyFinishes)
                finish.Init(playerHealthProxy);
        }

        private void BindFactories()
        {
            var prefabReferences = _container.Resolve<PrefabReferencesConfig>();
            var pauseService = _container.Resolve<IPauseService>();
            var mapCoordinator = _container.Resolve<MapCoordinator>();
            var input = _container.Resolve<IInput>();
            var playerBuildingCurrency = _container.Resolve<PlayerBuildingCurrencyProxy>();
            
            var towerFactory = new TowerFactory(
                _container, 
                _level.LevelConfig.TowerGenerationConfigs,
                prefabReferences,
                mapCoordinator,
                input,
                _level.TowersParent,
                pauseService);
            
            _container.Bind<TowerFactory>().FromInstance(towerFactory).AsSingle().NonLazy();
            
            var enemyFactory = new EnemyFactory(_container, prefabReferences.EnemyPrefab, playerBuildingCurrency, _level.EnemiesParent);
            _container.Bind<EnemyFactory>().FromInstance(enemyFactory).AsSingle().NonLazy();
            
            MergeHandler.Init(towerFactory);
        }

        private void BindServices()
        {
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            var playerBuildingCurrencyProxy = _container.Resolve<PlayerBuildingCurrencyProxy>();
            var pauseService = _container.Resolve<IPauseService>();
            
            var scoreService = new ScoreService(playerHealthProxy, playerBuildingCurrencyProxy);
            
            bindWaveSpawnerService();
            bindGameTimerService();
            bindRewardCalculatorService();

            _container.Bind<IScoreService>().To<ScoreService>().FromInstance(scoreService).AsSingle().NonLazy();
            _container.Bind<IGameStateService>().To<GameStateService>().FromNew().AsSingle().NonLazy();
            _container.Bind<IGameSpeedService>().To<GameSpeedService>().FromNew().AsSingle().NonLazy();

            void bindWaveSpawnerService()
            {
                var enemyFactory = _container.Resolve<EnemyFactory>();
                
                List<WaveSpawnerService> spawnerServices = new List<WaveSpawnerService>();

                for (var i = 0; i < _level.Paths.Length; i++)
                {
                    var pathData = _level.Paths[i];
                    List<Vector3> path = pathData.PathPoints.Select(x => x.position).ToList();

                    WaveSpawnerService spawnerService = new WaveSpawnerService(
                        enemyFactory,
                        _level.LevelConfig.WaveDatas[i].Waves,
                        path,
                        pathData.EnemySpawnPosition.position,
                        _monoBehaviourWrapper);

                    spawnerServices.Add(spawnerService);
                }

                foreach (var spawnerService in spawnerServices)
                    pauseService.Register(spawnerService);

                _container.Bind<IWaveSpawnerService[]>().FromInstance(spawnerServices.ToArray()).AsSingle().NonLazy();
            }

            void bindGameTimerService()
            {
                var monoBehaviour = _container.Resolve<MonoBehaviourWrapper>();
                
                GameTimerService gameTimerService = new GameTimerService(monoBehaviour);
                _container.Bind<IGameTimerService>().To<GameTimerService>().FromInstance(gameTimerService).AsSingle().NonLazy();
                
                pauseService.Register(gameTimerService);
            }

            void bindRewardCalculatorService()
            {
                var gameStateProvider = _container.Resolve<IGameStateProvider>();
                var waveSpawnServices = _container.Resolve<IWaveSpawnerService[]>();
                
                RewardCalculatorService service = new RewardCalculatorService(scoreService, _currentLevelIndex, _level.LevelConfig, gameStateProvider, waveSpawnServices);
                _container.Bind<IRewardCalculatorService>().To<RewardCalculatorService>().FromInstance(service).AsSingle().NonLazy();
            }
        }

        private void BindCameraSystem()
        {
            var input = _container.Resolve<IInput>();
            var pauseService = _container.Resolve<IPauseService>();
            var mapCoordinator = _container.Resolve<MapCoordinator>();
            
            var instance = new GameObject("CameraSystem");
            var cameraMovement = instance.AddComponent<CameraMovement>();

            _mainCamera.transform.SetParent(instance.transform);
            cameraMovement.Init(_mainCamera, _level.BaseTileMap, input, mapCoordinator);
            
            pauseService.Register(cameraMovement);
        }
    }
}