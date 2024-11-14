using R3;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TS.PageSlider;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.UI
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        private const int MAX_EPISODE_LEVELS = 21;

        [Header("Player currencies")]
        [SerializeField] private PlayerCoinsView _playerCoinsView;
        [SerializeField] private PlayerGemsView _playerGemsView;
        [Space(10)]
        [Header("Panels")]
        [SerializeField] private MainMenuPanelView _mainMenuPanelView;
        [SerializeField] private LevelsPanelView _levelsPanelView;
        [SerializeField] private EpisodeView _episodeViewPrefab;
        [SerializeField] private Transform _allLevelsParent;
        [SerializeField] private GameObject _levelsContainerPrefab;
        [SerializeField] private LevelEntryView _levelEntryViewPrefab;
        [Space(10)]
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
        private PlayerCoinsProxy _playerCoinsProxy;
        private PlayerGemsProxy _playerGemsProxy;

        private void HandleGoToGameplayButtonClicked(int levelNumber)
        {
            _exitSceneSignalBus?.OnNext(levelNumber);
        }

        public void Bind(ReactiveProperty<int> exitSceneSignalBus, DiContainer container)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _projectConfig = container.Resolve<ProjectConfig>();
            _gameStateProvider = container.Resolve<IGameStateProvider>();
            _playerCoinsProxy = container.Resolve<PlayerCoinsProxy>();
            _playerGemsProxy = container.Resolve<PlayerGemsProxy>();

            _gameStateProvider.SaveGameState();
            
            BindAdapters();
        }

        private void BindAdapters()
        {
            BindLevelEntryViewAdapters();

            new MainMenuPanelsViewAdapter(_mainMenuPanelView, _levelsPanelView, _settingsPopupView, _shopPopupView);
            new LevelsPanelViewAdapter(_levelsPanelView);
            new SettingsPopupViewAdapter(_settingsPopupView);
            new LevelLockPopupViewAdapter(_levelLockPopupView);
            
            var shopAdapter = new ShopPopupViewAdapter(_shopPopupView, _shopTowersView, _shopCoinView, _shopGemView);
            BindPlayerCurrencies(shopAdapter);
        }

        private void BindLevelEntryViewAdapters()
        {
            int episodesCount = Mathf.CeilToInt((float)_projectConfig.Levels.Length / MAX_EPISODE_LEVELS);

            int createdLevelCounter = 0;
            for (int i = 0; i < episodesCount; i++)
            {
                var episodeView = Instantiate(_episodeViewPrefab, _allLevelsParent);
                episodeView.SetEpisodeNumberText(i + 1);
                
                var currentLevelsContainer = Instantiate(_levelsContainerPrefab, _allLevelsParent);

                for (int j = 0; j < MAX_EPISODE_LEVELS; j++)
                {
                    if(createdLevelCounter >= _projectConfig.Levels.Length)
                        return;
                    
                    var levelSaveDataProxy = _gameStateProvider.GameState.LevelDatas[createdLevelCounter];
                    
                    var viewInstance = Instantiate(_levelEntryViewPrefab, currentLevelsContainer.transform);
                    viewInstance.name = $"LevelEntry: {createdLevelCounter + 1}";
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

        private void BindPlayerCurrencies(ShopPopupViewAdapter shopAdapter)
        {
            new PlayerCoinsViewAdapter(_playerCoinsView, _playerCoinsProxy, _shopPopupView, shopAdapter);
            new PlayerGemsViewAdapter(_playerGemsView, _playerGemsProxy, _shopPopupView, shopAdapter);
        }
    }
}
