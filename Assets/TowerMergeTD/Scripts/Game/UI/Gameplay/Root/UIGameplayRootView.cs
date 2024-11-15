using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using TowerMergeTD.GameRoot;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private PlayerBuildingCurrencyView _buildingCurrencyView;
        [SerializeField] private Button _pauseButton;

        [Header("Other")]
        [SerializeField] private Transform _otherObjectsParent;
        [SerializeField] private TowerSellView _towerSellView;
        [SerializeField] private TowersListView _towersListView;
        [SerializeField] private GameTimerView _gameTimerView;
        [SerializeField] private TutorialView tutorialViewPrefab;

        [Header("Popups")]
        [SerializeField] private PausePopupView _pausePopupView;
        [SerializeField] private LosePopupView _losePopupView;
        [SerializeField] private LevelCompletePopupView _levelCompletePopupView;

        private DiContainer _container;
        private ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private int _currentLevelIndex;
        private ProjectConfig _projectConfig;
        private LevelConfig _currentLevelConfig;

        public void Bind(ReactiveProperty<SceneEnterParams> exitSceneSignalBus, DiContainer container, int currentLevelIndex)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _container = container;
            _currentLevelIndex = currentLevelIndex;
            
            _projectConfig = _container.Resolve<ProjectConfig>();
            _currentLevelConfig = _projectConfig.Levels[_currentLevelIndex].LevelConfig;

            if (_currentLevelConfig.IsTutorial)
                BindTutorial();
            
            BindAdapters();
        }

        private void BindTutorial()
        {
            var tutorialTextView = Instantiate(tutorialViewPrefab, _otherObjectsParent);
            
            _container.Bind<TutorialView>().FromInstance(tutorialTextView).AsSingle().NonLazy();
            _container.Bind<TowersListView>().FromInstance(_towersListView).AsSingle().NonLazy();
            _container.Bind<TowerSellView>().FromInstance(_towerSellView).AsSingle().NonLazy();
        }
        
        private void BindAdapters()
        {
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            var buildingCurrencyProxy = _container.Resolve<PlayerBuildingCurrencyProxy>();
            var pauseService = _container.Resolve<IPauseService>();
            var gameStateService = _container.Resolve<IGameStateService>();
            var gameTimerService = _container.Resolve<IGameTimerService>();

            new PlayerBuildingCurrencyViewAdapter(_buildingCurrencyView, buildingCurrencyProxy);
            new PlayerHealthViewAdapter(_playerHealthView, playerHealthProxy);
            new GameTimerViewAdapter(_gameTimerView, gameTimerService);
            
            bindTowerActions();
            bindPausePopup();
            bindLoseGamePopup();
            bindLevelCompletePopup();
            
            void bindTowerActions()
            {
                var inputHandler = _container.Resolve<IInput>();
                var towerFactory = _container.Resolve<TowerFactory>();
                var mapCoordinator = _container.Resolve<MapCoordinator>();

                new TowerActionsAdapter(_towerSellView, _towersListView, inputHandler, towerFactory, mapCoordinator, buildingCurrencyProxy, pauseService);
            }

            void bindPausePopup()
            {
                new PausePopupViewAdapter(_pausePopupView, _currentLevelIndex, _pauseButton, _exitSceneSignalBus, pauseService);
            }

            void bindLoseGamePopup()
            {
                var losePopupViewAdapter = new LosePopupViewAdapter(_losePopupView, _currentLevelIndex, _exitSceneSignalBus);
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
                    isLastLevel,
                    _currentLevelIndex,
                    _exitSceneSignalBus,
                    _currentLevelConfig,
                    scoreService,
                    rewardCalculate,
                    gameTimerService
                    );
                
                gameStateService.Register(levelCompletePopupAdapter);
            }
        }
    }
}
