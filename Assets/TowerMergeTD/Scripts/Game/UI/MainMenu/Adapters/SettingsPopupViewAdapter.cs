namespace TowerMergeTD.Game.UI
{
    public class SettingsPopupViewAdapter
    {
        private readonly SettingsPopupView _view;

        public SettingsPopupViewAdapter(SettingsPopupView view)
        {
            _view = view;

            _view.OnCloseButtonClicked += HandleSettingCloseButtonClicked;
        }

        private void HandleSettingCloseButtonClicked()
        {
            _view.Hide();
        }
    }
}