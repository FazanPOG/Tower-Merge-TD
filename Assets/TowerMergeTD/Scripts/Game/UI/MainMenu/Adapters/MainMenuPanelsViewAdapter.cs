using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class MainMenuPanelsViewAdapter
    {
        private readonly LevelsPanelView _levelsPanelView;
        private readonly SettingsPopupView _settingsPopupView;
        private readonly ShopPopupView _shopPopupView;
        private readonly AudioPlayer _audioPlayer;

        public MainMenuPanelsViewAdapter(
            MainMenuPanelView mainMenuPanelView, 
            LevelsPanelView levelsPanelView,
            SettingsPopupView settingsPopupView,
            ShopPopupView shopPopupView,
            ILocalizationAsset localizationAsset,
            AudioPlayer audioPlayer)
        {
            _levelsPanelView = levelsPanelView;
            _settingsPopupView = settingsPopupView;
            _shopPopupView = shopPopupView;
            _audioPlayer = audioPlayer;

            mainMenuPanelView.SetGameTitleText(localizationAsset.GetTranslation(LocalizationKeys.TOWER_MERGE_TD_KEY));
            mainMenuPanelView.SetShopText(localizationAsset.GetTranslation(LocalizationKeys.SHOP_KEY));
            mainMenuPanelView.SetSettingsText(localizationAsset.GetTranslation(LocalizationKeys.SETTINGS_KEY));
            
            mainMenuPanelView.OnPlayButtonClicked += HandlePlayButtonClicked;
            mainMenuPanelView.OnSettingsButtonClicked += HandleSettingsButtonClicked;
            mainMenuPanelView.OnShopButtonClicked += HandleShopButtonClicked;
        }

        private void HandlePlayButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _levelsPanelView.Show();
        }

        private void HandleSettingsButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _settingsPopupView.Show();
        }

        private void HandleShopButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _shopPopupView.Show();
        }
    }
}