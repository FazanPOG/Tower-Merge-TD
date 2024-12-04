﻿using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class SettingsPopupViewAdapter
    {
        private readonly SettingsPopupView _view;
        private readonly ILocalizationAsset _localizationAsset;
        private readonly IGameStateProvider _gameStateProvider;

        public SettingsPopupViewAdapter(
            SettingsPopupView view, 
            ILocalizationAsset localizationAsset,
            IGameStateProvider gameStateProvider)
        {
            _view = view;
            _localizationAsset = localizationAsset;
            _gameStateProvider = gameStateProvider;
                
            UpdateView();
            
            _view.OnCloseButtonClicked += HandleSettingCloseButtonClicked;
            _view.OnSoundSliderChanged += OnSoundSliderChanged;
            _view.OnMusicSliderChanged += OnMusicSliderChanged;
        }

        private void UpdateView()
        {
            _view.SetSettingsText(_localizationAsset.GetTranslation(LocalizationKeys.SETTINGS_KEY));
            _view.SetSoundText(_localizationAsset.GetTranslation(LocalizationKeys.SOUND_KEY));
            _view.SetMusicText(_localizationAsset.GetTranslation(LocalizationKeys.MUSIC_KEY));
            _view.SetSoundSliderValue(_gameStateProvider.GameState.SoundVolume.Value);
            _view.SetMusicSliderValue(_gameStateProvider.GameState.MusicVolume.Value);
        }

        private void OnSoundSliderChanged(float newValue)
        {
            _gameStateProvider.GameState.SoundVolume.Value = newValue;
            _gameStateProvider.SaveGameState();
        }

        private void OnMusicSliderChanged(float newValue)
        {
            _gameStateProvider.GameState.MusicVolume.Value = newValue;
            _gameStateProvider.SaveGameState();
        }

        private void HandleSettingCloseButtonClicked()
        {
            _view.Hide();
        }
    }
}