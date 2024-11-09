namespace TowerMergeTD.Game.UI
{
    public class MainMenuPanelsViewAdapter
    {
        private readonly LevelsPanelView _levelsPanelView;
        private readonly SettingsPopupView _settingsPopupView;
        private readonly ShopPopupView _shopPopupView;

        public MainMenuPanelsViewAdapter(
            MainMenuPanelView mainMenuPanelView, 
            LevelsPanelView levelsPanelView,
            SettingsPopupView settingsPopupView,
            ShopPopupView shopPopupView)
        {
            _levelsPanelView = levelsPanelView;
            _settingsPopupView = settingsPopupView;
            _shopPopupView = shopPopupView;

            mainMenuPanelView.OnPlayButtonClicked += HandlePlayButtonClicked;
            mainMenuPanelView.OnSettingsButtonClicked += HandleSettingsButtonClicked;
            mainMenuPanelView.OnShopButtonClicked += HandleShopButtonClicked;
        }

        private void HandlePlayButtonClicked()
        {
            _levelsPanelView.Show();
        }

        private void HandleSettingsButtonClicked()
        {
            _settingsPopupView.Show();
        }

        private void HandleShopButtonClicked()
        {
            _shopPopupView.Show();
        }
    }
}