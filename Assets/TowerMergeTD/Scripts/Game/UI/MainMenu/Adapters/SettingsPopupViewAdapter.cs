using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class SettingsPopupViewAdapter
    {
        private readonly SettingsPopupView _view;

        public SettingsPopupViewAdapter(SettingsPopupView view, ILocalizationAsset localizationAsset)
        {
            _view = view;

            _view.SetSettingsText(localizationAsset.GetTranslation(LocalizationKeys.SETTINGS_KEY));
            _view.SetSoundText(localizationAsset.GetTranslation(LocalizationKeys.SOUND_KEY));
            _view.SetMusicText(localizationAsset.GetTranslation(LocalizationKeys.MUSIC_KEY));
            
            _view.OnCloseButtonClicked += HandleSettingCloseButtonClicked;
        }

        private void HandleSettingCloseButtonClicked()
        {
            _view.Hide();
        }
    }
}