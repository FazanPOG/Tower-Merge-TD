using System;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class LevelEntryViewAdapter
    {
        private readonly bool _isDevelopmentSettings;
        private readonly LevelEntryView _levelEntryView;
        private readonly LevelLockPopupView _levelLockPopupView;
        private readonly LevelSaveDataProxy _levelSaveDataProxy;
        private readonly LevelConfig _levelConfig;
        private readonly Action<int> _goGameplayCallback;

        public LevelEntryViewAdapter(
            bool isDevelopmentSettings, 
            LevelEntryView levelEntryView,
            LevelLockPopupView levelLockPopupView,
            LevelSaveDataProxy levelSaveDataProxy, 
            LevelConfig levelConfig, 
            Action<int> goGameplayCallback)
        {
            _isDevelopmentSettings = isDevelopmentSettings;
            _levelEntryView = levelEntryView;
            _levelLockPopupView = levelLockPopupView;
            _levelSaveDataProxy = levelSaveDataProxy;
            _levelConfig = levelConfig;
            _goGameplayCallback = goGameplayCallback;

            Init();
        }

        private void Init()
        {
            _levelEntryView.SetLevelText($"{_levelSaveDataProxy.ID + 1}");

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
                _levelLockPopupView.SetLevelNumberText($"Level: {_levelSaveDataProxy.ID + 1}");
            };
        }

        private void OpenStageView()
        {
            _levelEntryView.SetActiveDefaultStage(true);
            _levelEntryView.SetActiveLockStage(false);
            _levelEntryView.SetActiveCompleteStage(false);
            
            _levelEntryView.OnButtonClicked += () => _goGameplayCallback.Invoke(_levelSaveDataProxy.ID);
        }

        private void CompleteStageView()
        {
            _levelEntryView.SetActiveCompleteStage(true);
            _levelEntryView.SetActiveDefaultStage(false);
            _levelEntryView.SetActiveLockStage(false);
            
            _levelEntryView.OnButtonClicked += () => _goGameplayCallback.Invoke(_levelSaveDataProxy.ID);
            
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
