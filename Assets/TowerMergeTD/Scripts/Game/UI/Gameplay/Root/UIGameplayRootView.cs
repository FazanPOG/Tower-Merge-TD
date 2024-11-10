using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils.Debug;
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
        [SerializeField] private TowerActionsView _towerActionsView;
        [SerializeField] private GameplayInputDebugView _debugInputView;

        [Header("Popups")]
        [SerializeField] private PausePopupView _pausePopupView;
        [SerializeField] private LosePopupView _losePopupView;
        [SerializeField] private LevelCompletePopupView _levelCompletePopupView;

        private DiContainer _container;
        private ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private int _currentLevelIndex;

        public void Bind(ReactiveProperty<SceneEnterParams> exitSceneSignalBus, DiContainer container, int currentLevelIndex)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _container = container;
            _currentLevelIndex = currentLevelIndex;
            
            BindAdapters();
        }

        private void BindAdapters()
        {
            var projectConfig = _container.Resolve<ProjectConfig>();
            var playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            var buildingCurrencyProxy = _container.Resolve<PlayerBuildingCurrencyProxy>();
            var pauseService = _container.Resolve<IPauseService>();
            var gameStateService = _container.Resolve<IGameStateService>();

            new PlayerBuildingCurrencyViewAdapter(_buildingCurrencyView, buildingCurrencyProxy);
            new PlayerHealthViewAdapter(_playerHealthView, playerHealthProxy);
            
            bindTowerActions();
            bindPausePopup();
            bindLoseGamePopup();
            bindLevelCompletePopup();

            //TODO: remove
            //bindDebugView();
            
            void bindTowerActions()
            {
                var inputHandler = _container.Resolve<IInput>();
                var towerFactory = _container.Resolve<TowerFactory>();
                var mapCoordinator = _container.Resolve<MapCoordinator>();

                new TowerActionsAdapter(_towerActionsView, inputHandler, towerFactory, mapCoordinator, buildingCurrencyProxy, pauseService);
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
                var levelConfig = projectConfig.Levels[_currentLevelIndex].LevelConfig;
                bool isLastLevel = _currentLevelIndex + 1 == projectConfig.Levels.Length;
                var scoreService = _container.Resolve<IScoreService>();
                var rewardCalculate = _container.Resolve<IRewardCalculatorService>();
                
                var levelCompletePopupAdapter = new LevelCompletePopupAdapter
                    (
                    _levelCompletePopupView, 
                    isLastLevel,
                    _currentLevelIndex,
                    _exitSceneSignalBus,
                    levelConfig,
                    scoreService,
                    rewardCalculate
                    );
                
                gameStateService.Register(levelCompletePopupAdapter);
            }

            void bindDebugView()
            {
                var input = _container.Resolve<IInput>();
                _debugInputView.gameObject.SetActive(true);
                _debugInputView.Init(input);
            }
        }
    }
}
