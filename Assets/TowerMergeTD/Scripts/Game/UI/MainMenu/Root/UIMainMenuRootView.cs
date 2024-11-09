using R3;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TS.PageSlider;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        private const int MAX_LEVELS_ON_PAGE = 21;
        
        [Header("Panels")]
        [SerializeField] private MainMenuPanelView _mainMenuPanelView;
        [SerializeField] private LevelsPanelView _levelsPanelView;
        [SerializeField] private PageSlider _pageSlider;
        [SerializeField] private PageView _pagePrefab;
        [SerializeField] private LevelEntryView _levelEntryViewPrefab;
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
            BindLevelEntryViewAdapters();
            
            new MainMenuPanelsViewAdapter(_mainMenuPanelView, _levelsPanelView, _settingsPopupView, _shopPopupView);
            new LevelsPanelViewAdapter(_levelsPanelView);
            new SettingsPopupViewAdapter(_settingsPopupView);
            new LevelLockPopupViewAdapter(_levelLockPopupView);
            new ShopPopupViewAdapter(_shopPopupView, _shopTowersView, _shopCoinView, _shopGemView);
        }

        private void BindLevelEntryViewAdapters()
        {
            int pagesCount = Mathf.CeilToInt((float)_projectConfig.Levels.Length / MAX_LEVELS_ON_PAGE);

            int createdLevelCounter = 0;
            for (int i = 0; i < pagesCount; i++)
            {
                var container = Instantiate(_pagePrefab);
                _pageSlider.AddPage((RectTransform)container.transform);
                
                for (int j = 0; j < MAX_LEVELS_ON_PAGE; j++)
                {
                    if(createdLevelCounter >= _projectConfig.Levels.Length)
                        return;
                    
                    var levelSaveDataProxy = _gameStateProvider.GameState.LevelDatas[createdLevelCounter];
                    var viewInstance = Instantiate(_levelEntryViewPrefab, container.transform);
                    var levelConfig = _projectConfig.Levels[createdLevelCounter].LevelConfig;
                
                    new LevelEntryViewAdapter
                    (
                        _projectConfig.IsDevelopmentSettings, 
                        viewInstance, 
                        _levelLockPopupView, 
                        levelSaveDataProxy, 
                        levelConfig, 
                        HandleGoToGameplayButtonClicked
                    );

                    createdLevelCounter++;
                }
            }
        }
    }
}
