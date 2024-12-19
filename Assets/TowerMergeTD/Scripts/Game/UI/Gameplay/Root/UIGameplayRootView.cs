using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using TowerMergeTD.GameRoot;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private PlayerBuildingCurrencyView _buildingCurrencyView;
        [SerializeField] private WavesCounterView _wavesCounterView;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GameSpeedView _gameSpeedView;

        [Header("Other")]
        [SerializeField] private Transform _otherObjectsParent;
        [SerializeField] private TowerSellView _towerSellView;
        [SerializeField] private TowersListView _towersListView;
        [SerializeField] private GameTimerView _gameTimerView;
        [FormerlySerializedAs("tutorialViewPrefab")] 
        [SerializeField] private TutorialView _tutorialViewPrefab;

        [Header("Popups")]
        [SerializeField] private PausePopupView _pausePopupView;
        [SerializeField] private LosePopupView _losePopupView;
        [SerializeField] private LevelCompletePopupView _levelCompletePopupView;
        [SerializeField] private AdConfirmationPopupView _adConfirmationPopupView;

        private DiContainer _container;
        private ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private int _currentLevelIndex;
        private ProjectConfig _projectConfig;
        private LevelConfig _currentLevelConfig;
        private bool _isTutorialLevel;
        private List<IDisposable> _disposables = new List<IDisposable>();
        
        public void Bind(ReactiveProperty<SceneEnterParams> exitSceneSignalBus, DiContainer container, int currentLevelIndex)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _container = container;
            _currentLevelIndex = currentLevelIndex;
            
            _projectConfig = _container.Resolve<ProjectConfig>();
            _currentLevelConfig = _projectConfig.Levels[_currentLevelIndex].LevelConfig;
            _isTutorialLevel = _currentLevelConfig.IsTutorial;
            
            if (_isTutorialLevel)
                BindTutorial();
            
            BindAdapters();
        }

        private void BindTutorial()
        {
            var tutorialTextView = Instantiate(_tutorialViewPrefab, _otherObjectsParent);
            
            _container.Bind<TutorialView>().FromInstance(tutorialTextView).AsSingle().NonLazy();
            _container.Bind<TowersListView>().FromInstance(_towersListView).AsSingle().NonLazy();
            _container.Bind<TowerSellView>().FromInstance(_towerSellView).AsSingle().NonLazy();
        }
        
        private void BindAdapters()
        {
            var localizationAsset = _container.Resolve<ILocalizationAsset>();
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            var playerCoinsProxy = _container.Resolve<PlayerCoinsProxy>();
            var buildingCurrencyProxy = _container.Resolve<PlayerBuildingCurrencyProxy>();
            var pauseService = _container.Resolve<IPauseService>();
            var gameStateService = _container.Resolve<IGameStateService>();
            var gameTimerService = _container.Resolve<IGameTimerService>();
            var gameSpeedService = _container.Resolve<IGameSpeedService>();
            var waveSpawnerServices = _container.Resolve<IWaveSpawnerService[]>();
            var adService = _container.Resolve<IADService>();
            var audioPlayer = _container.Resolve<AudioPlayer>();

            var buildingCurrencyViewAdapter = new PlayerBuildingCurrencyViewAdapter(
                _buildingCurrencyView, 
                buildingCurrencyProxy,
                _adConfirmationPopupView,
                localizationAsset,
                adService,
                pauseService
                );
            
            var healthViewAdapter = new PlayerHealthViewAdapter(
                _playerHealthView, 
                playerHealthProxy,
                _adConfirmationPopupView,
                localizationAsset,
                adService,
                pauseService);
            
            _disposables.Add(buildingCurrencyViewAdapter);
            _disposables.Add(healthViewAdapter);
            
            new GameTimerViewAdapter(_gameTimerView, gameTimerService);
            new GameSpeedViewAdapter(gameSpeedService, _gameSpeedView);
            new WavesCounterViewAdapter(waveSpawnerServices, _wavesCounterView);
            
            bindTowerActions();
            bindPausePopup();
            bindLoseGamePopup();
            bindLevelCompletePopup();
            
            void bindTowerActions()
            {
                var gameStateProxy = _container.Resolve<IGameStateProvider>().GameState;
                var inputHandler = _container.Resolve<IInput>();
                var towerFactory = _container.Resolve<TowerFactory>();
                var mapCoordinator = _container.Resolve<MapCoordinator>();

                TowerType[] types = gameStateProxy.UnlockTowers.ToArray();

                var towerActionsAdapter = new TowerActionsAdapter(
                    _isTutorialLevel,
                    types, 
                    _towerSellView, 
                    _towersListView, 
                    inputHandler, 
                    towerFactory, 
                    mapCoordinator, 
                    buildingCurrencyProxy, 
                    pauseService,
                    audioPlayer);

                _container.Bind<TowerActionsAdapter>().FromInstance(towerActionsAdapter).AsCached().NonLazy();
            }

            void bindPausePopup()
            {
                new PausePopupViewAdapter(
                    _pausePopupView, 
                    _currentLevelIndex, 
                    _pauseButton, 
                    _exitSceneSignalBus, 
                    pauseService, 
                    localizationAsset, 
                    _projectConfig.IsDevelopmentSettings, 
                    adService
                    );
            }

            void bindLoseGamePopup()
            {
                var losePopupViewAdapter = new LosePopupViewAdapter(_losePopupView, _currentLevelIndex, _exitSceneSignalBus, localizationAsset, audioPlayer, playerCoinsProxy);
                gameStateService.Register(losePopupViewAdapter);
            }

            void bindLevelCompletePopup()
            {
                bool isLastLevel = _currentLevelIndex + 1 == _projectConfig.Levels.Length;
                var scoreService = _container.Resolve<IScoreService>();
                var rewardCalculate = _container.Resolve<IRewardCalculatorService>();

                var levelCompletePopupAdapter = new LevelCompletePopupAdapter
                    (
                    _levelCompletePopupView, 
                    _projectConfig.IsDevelopmentSettings,
                    isLastLevel,
                    _currentLevelIndex,
                    _exitSceneSignalBus,
                    _currentLevelConfig,
                    scoreService,
                    rewardCalculate,
                    gameTimerService,
                    localizationAsset,
                    adService,
                    audioPlayer
                    );
                
                gameStateService.Register(levelCompletePopupAdapter);
            }
        }

        private void OnDisable()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
