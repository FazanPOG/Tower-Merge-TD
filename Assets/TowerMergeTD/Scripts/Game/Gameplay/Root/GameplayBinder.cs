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
        
        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private Camera _mainCamera;
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public GameStateMachine Bind(Level level)
        {
            _level = level;
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
            var spawnerServices = _container.Resolve<IWaveSpawnerService[]>();
            var pauseService = _container.Resolve<IPauseService>();
            var gameStateService = _container.Resolve<IGameStateService>();
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            
            GameStateMachine gameStateMachine = new GameStateMachine(spawnerServices, pauseService, playerHealthProxy, gameStateService);

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
            _container.BindInterfacesTo<DesktopInput>().FromNew().AsSingle().WithArguments(_mainCamera).NonLazy();
        }

        private void BindMap()
        {
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            var input = _container.Resolve<IInput>();
            
            var mapCoordinator = new MapCoordinator(_level.BaseTileMap, _level.EnvironmentTileMap, _level.LevelConfig.TileSetConfig, input);
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
            
            var towerFactory = new TowerFactory(
                _container, 
                _level.LevelConfig.TowerGenerationConfigs,
                prefabReferences,
                mapCoordinator,
                input,
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
        }

        private void BindCameraSystem()
        {
            var instance = new GameObject("CameraSystem");
            var cameraMovement = instance.AddComponent<CameraMovement>();
            var input = _container.Resolve<IInput>();
            
            _mainCamera.transform.SetParent(instance.transform);
            cameraMovement.Init(_mainCamera, _level.BaseTileMap, input);
        }
    }
}