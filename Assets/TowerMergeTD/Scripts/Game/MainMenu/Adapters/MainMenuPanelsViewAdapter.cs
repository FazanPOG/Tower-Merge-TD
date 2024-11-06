namespace TowerMergeTD.Game.MainMenu
{
    public class MainMenuPanelsViewAdapter
    {
        private readonly LevelsPanelView _levelsPanelView;
        private readonly SettingsPopupView _settingsPopupView;

        public MainMenuPanelsViewAdapter(
            MainMenuPanelView mainMenuPanelView, 
            LevelsPanelView levelsPanelView,
            SettingsPopupView settingsPopupView)
        {
            _levelsPanelView = levelsPanelView;
            _settingsPopupView = settingsPopupView;

            mainMenuPanelView.OnPlayButtonClicked += HandlePlayButtonClicked;
            mainMenuPanelView.OnSettingsButtonClicked += HandleSettingsButtonClicked;
        }

        private void HandlePlayButtonClicked()
        {
            _levelsPanelView.Show();
        }

        private void HandleSettingsButtonClicked()
        {
            _settingsPopupView.Show();
        }
    }
}