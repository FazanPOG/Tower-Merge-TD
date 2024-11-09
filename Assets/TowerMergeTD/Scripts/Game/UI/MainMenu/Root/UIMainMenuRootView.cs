using R3;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private MainMenuPanelView _mainMenuPanelView;
        [SerializeField] private LevelsPanelView _levelsPanelView;
        [SerializeField] private LevelEntryView _levelEntryViewPrefab;
        [SerializeField] private Transform _levelEntryViewParent;
        [Header("Popups")]
        [SerializeField] private SettingsPopupView _settingsPopupView;
        [SerializeField] private LevelLockPopupView _levelLockPopupView;
        [SerializeField] private ShopPopupView _shopPopupView;
        [SerializeField] private ShopTowersView _shopTowersView;
        [SerializeField] private ShopCoinView _shopCoinView;
        [SerializeField] private ShopGemView _shopGemView;

        private ReactiveProperty<int> _exitSceneSignalBus;
        private ProjectConfig _projectConfig;
        private IGameStateProvider _gameStateProvider;

        private void HandleGoToGameplayButtonClicked(int levelNumber)
        {
            _exitSceneSignalBus?.OnNext(levelNumber);
        }

        public void Bind(ReactiveProperty<int> exitSceneSignalBus, ProjectConfig projectConfig, IGameStateProvider gameStateProvider)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _projectConfig = projectConfig;
            _gameStateProvider = gameStateProvider;
            
            BindAdapters();
        }

        private void BindAdapters()
        {
            for (var i = 0; i < _projectConfig.Levels.Length; i++)
            {
                var levelSaveDataProxy = _gameStateProvider.GameState.LevelDatas[i];
                var viewInstance = Instantiate(_levelEntryViewPrefab, _levelEntryViewParent);
                var levelConfig = _projectConfig.Levels[i].LevelConfig;
                
                new LevelEntryViewAdapter
                    (
                    _projectConfig.IsDevelopmentSettings, 
                    viewInstance, 
                    _levelLockPopupView, 
                    levelSaveDataProxy, 
                    levelConfig, 
                    HandleGoToGameplayButtonClicked
                    );
            }

            new MainMenuPanelsViewAdapter(_mainMenuPanelView, _levelsPanelView, _settingsPopupView, _shopPopupView);
            new LevelsPanelViewAdapter(_levelsPanelView);
            new SettingsPopupViewAdapter(_settingsPopupView);
            new LevelLockPopupViewAdapter(_levelLockPopupView);
            new ShopPopupViewAdapter(_shopPopupView, _shopTowersView, _shopCoinView, _shopGemView);
        }
    }
}
