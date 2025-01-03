using System;
using System.Collections.Generic;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
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
        [SerializeField] private TowerInfoPopupView _towerInfoPopupView;
        [Header("Shop")]
        [SerializeField] private ShopTowersView _shopTowersView;
        [SerializeField] private ShopCoinView _shopCoinView;
        [SerializeField] private ShopGemView _shopGemView;
        
        private ShopItemView[] _shopItemViews;
        
        private ReactiveProperty<int> _exitSceneSignalBus;
        private ProjectConfig _projectConfig;
        private IGameStateProvider _gameStateProvider;
        private ICurrencyProvider _currencyProvider;
        private PlayerCoinsProxy _playerCoinsProxy;
        private PlayerGemsProxy _playerGemsProxy;
        private ILocalizationAsset _localizationAsset;
        private IADService _adService;
        private ShopPopupViewAdapter _shopPopupViewAdapter;
        private AudioPlayer _audioPlayer;
        private List<IDisposable> _disposables = new List<IDisposable>();

        public void Bind(ReactiveProperty<int> exitSceneSignalBus, DiContainer container)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _projectConfig = container.Resolve<ProjectConfig>();
            _gameStateProvider = container.Resolve<IGameStateProvider>();
            _currencyProvider = container.Resolve<ICurrencyProvider>();
            _playerCoinsProxy = container.Resolve<PlayerCoinsProxy>();
            _playerGemsProxy = container.Resolve<PlayerGemsProxy>();
            _localizationAsset = container.Resolve<ILocalizationAsset>();
            _adService = container.Resolve<IADService>();
            _audioPlayer = container.Resolve<AudioPlayer>();

            _shopItemViews = GetComponentsInChildren<ShopItemView>(true);
            
            _gameStateProvider.SaveGameState();
            
            BindAdapters();
        }

        private void HandleGoToGameplayButtonClicked(int levelIndex) => _exitSceneSignalBus?.OnNext(levelIndex);

        private void BindAdapters()
        {
            BindLevelEntryViewAdapters();

            new MainMenuPanelsViewAdapter(_mainMenuPanelView, _levelsPanelView, _settingsPopupView, _shopPopupView, _localizationAsset, _audioPlayer);
            new LevelsPanelViewAdapter(_levelsPanelView, _audioPlayer);
            new SettingsPopupViewAdapter(_settingsPopupView, _localizationAsset, _gameStateProvider, _audioPlayer);
            new LevelLockPopupViewAdapter(_levelLockPopupView, _audioPlayer);
            new TowerInfoPopupViewAdapter(_towerInfoPopupView, _audioPlayer);
            
            _shopPopupViewAdapter = new ShopPopupViewAdapter(_shopPopupView, _shopTowersView, _shopCoinView, _shopGemView, _localizationAsset, _audioPlayer);
            BindPlayerCurrencies();
            BindShopItems();
        }

        private void BindLevelEntryViewAdapters()
        {
            int episodesCount = Mathf.CeilToInt((float)_projectConfig.Levels.Length / MAX_EPISODE_LEVELS);

            int createdLevelCounter = 0;
            for (int i = 0; i < episodesCount; i++)
            {
                var episodeView = Instantiate(_episodeViewPrefab, _allLevelsParent);
                episodeView.SetEpisodeNumberText($"{_localizationAsset.GetTranslation(LocalizationKeys.EPISODE_KEY)} {i + 1}");
                
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
                        createdLevelCounter,
                        _projectConfig.IsDevelopmentSettings, 
                        viewInstance, 
                        _levelLockPopupView, 
                        levelSaveDataProxy, 
                        levelConfig, 
                        HandleGoToGameplayButtonClicked,
                        _localizationAsset,
                        _audioPlayer
                    );

                    createdLevelCounter++;
                }
            }
        }

        private void BindPlayerCurrencies()
        {
            new PlayerCoinsViewAdapter(_playerCoinsView, _playerCoinsProxy, _shopPopupView, _shopPopupViewAdapter, _audioPlayer);
            new PlayerGemsViewAdapter(_playerGemsView, _playerGemsProxy, _shopPopupView, _shopPopupViewAdapter, _audioPlayer);
        }

        private void BindShopItems()
        {
            foreach (var shopItemView in _shopItemViews)
            {
                var shopItemViewAdapter = new ShopItemViewAdapter(
                    shopItemView,
                    _towerInfoPopupView,
                    _shopPopupViewAdapter,
                    _playerCoinsProxy, 
                    _playerGemsProxy, 
                    _gameStateProvider,
                    _currencyProvider,
                    _localizationAsset, 
                    _adService, 
                    _audioPlayer);
                
                _disposables.Add(shopItemViewAdapter);
            }
        }

        private void OnDisable()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}
