using System;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class LevelEntryViewAdapter
    {
        private readonly int _levelIndex;
        private readonly bool _isDevelopmentSettings;
        private readonly LevelEntryView _levelEntryView;
        private readonly LevelLockPopupView _levelLockPopupView;
        private readonly LevelSaveDataProxy _levelSaveDataProxy;
        private readonly LevelConfig _levelConfig;
        private readonly Action<int> _goGameplayCallback;
        private readonly ILocalizationAsset _localizationAsset;
        private readonly AudioPlayer _audioPlayer;

        public LevelEntryViewAdapter(
            int levelIndex,
            bool isDevelopmentSettings, 
            LevelEntryView levelEntryView,
            LevelLockPopupView levelLockPopupView,
            LevelSaveDataProxy levelSaveDataProxy, 
            LevelConfig levelConfig, 
            Action<int> goGameplayCallback,
            ILocalizationAsset localizationAsset,
            AudioPlayer audioPlayer)
        {
            _levelIndex = levelIndex;
            _isDevelopmentSettings = isDevelopmentSettings;
            _levelEntryView = levelEntryView;
            _levelLockPopupView = levelLockPopupView;
            _levelSaveDataProxy = levelSaveDataProxy;
            _levelConfig = levelConfig;
            _goGameplayCallback = goGameplayCallback;
            _localizationAsset = localizationAsset;
            _audioPlayer = audioPlayer;

            Init();
        }

        private void Init()
        {
            _levelEntryView.SetLevelText($"{_levelIndex + 1}");

            bool isOpen;
            if (_isDevelopmentSettings)
                isOpen = _levelConfig.IsOpen;
            else
                isOpen = _levelSaveDataProxy.IsOpen;    
            
            if (isOpen == false)
            {
                LockStageView();
            }
            else
            {
                if (_levelSaveDataProxy.Score == 0)
                    OpenStageView();
                else
                    CompleteStageView();
            }
        }

        private void LockStageView()
        {
            _levelEntryView.SetActiveLockStage(true);
            _levelEntryView.SetActiveDefaultStage(false);
            _levelEntryView.SetActiveCompleteStage(false);
            
            _levelEntryView.OnButtonClicked += () =>
            {
                _levelLockPopupView.Show();
                _levelLockPopupView.SetLevelNumberText($"{_localizationAsset.GetTranslation(LocalizationKeys.LEVEL_KEY)}: {_levelSaveDataProxy.ID + 1}");
                _levelLockPopupView.SetDescriptionText(_localizationAsset.GetTranslation(LocalizationKeys.LEVEL_LOCK_DESCRIPTION_KEY));
                _audioPlayer.Play(AudioType.Button);
            };
        }

        private void OpenStageView()
        {
            _levelEntryView.SetActiveDefaultStage(true);
            _levelEntryView.SetActiveLockStage(false);
            _levelEntryView.SetActiveCompleteStage(false);

            _levelEntryView.OnButtonClicked += () =>
            {
                _audioPlayer.Play(AudioType.Button);
                _goGameplayCallback.Invoke(_levelIndex);
            };
        }

        private void CompleteStageView()
        {
            _levelEntryView.SetActiveCompleteStage(true);
            _levelEntryView.SetActiveDefaultStage(false);
            _levelEntryView.SetActiveLockStage(false);
            
            _levelEntryView.OnButtonClicked += () =>
            {
                _audioPlayer.Play(AudioType.Button);
                _goGameplayCallback.Invoke(_levelIndex);
            };
            
            if(_levelSaveDataProxy.Score < _levelConfig.ScoreForOneStar)
                _levelEntryView.SetStars(0);
            else if(_levelSaveDataProxy.Score < _levelConfig.ScoreForTwoStars)
                _levelEntryView.SetStars(1);
            else if(_levelSaveDataProxy.Score < _levelConfig.ScoreForThreeStars)
                _levelEntryView.SetStars(2);
            else
                _levelEntryView.SetStars(3);
        }
    }
}
