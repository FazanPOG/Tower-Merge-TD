using System;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.MainMenu
{
    public class LevelEntryViewAdapter
    {
        private readonly bool _isDevelopmentSettings;
        private readonly LevelEntryView _view;
        private readonly LevelSaveDataProxy _levelSaveDataProxy;
        private readonly LevelConfig _levelConfig;
        private readonly Action<int> _buttonClickedCallback;

        public LevelEntryViewAdapter(bool isDevelopmentSettings, LevelEntryView view, LevelSaveDataProxy levelSaveDataProxy, LevelConfig levelConfig, Action<int> buttonClickedCallback)
        {
            _isDevelopmentSettings = isDevelopmentSettings;
            _view = view;
            _levelSaveDataProxy = levelSaveDataProxy;
            _levelConfig = levelConfig;
            _buttonClickedCallback = buttonClickedCallback;

            Init();
        }

        private void Init()
        {
            _view.SetLevelText(_levelSaveDataProxy.ID.ToString());

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

            _view.OnButtonClicked += () => _buttonClickedCallback.Invoke(_levelSaveDataProxy.ID);
        }

        private void LockStageView()
        {
            _view.SetActiveLockStage(true);
            _view.SetActiveDefaultStage(false);
            _view.SetActiveCompleteStage(false);
            _view.SetButtonInteractable(false);
        }

        private void OpenStageView()
        {
            _view.SetActiveDefaultStage(true);
            _view.SetActiveLockStage(false);
            _view.SetActiveCompleteStage(false);
            _view.SetButtonInteractable(true);
        }

        private void CompleteStageView()
        {
            _view.SetActiveCompleteStage(true);
            _view.SetActiveDefaultStage(false);
            _view.SetActiveLockStage(false);
            _view.SetButtonInteractable(true);
            
            if(_levelSaveDataProxy.Score < _levelConfig.ScoreForOneStar)
                _view.SetStars(0);
            else if(_levelSaveDataProxy.Score < _levelConfig.ScoreForTwoStars)
                _view.SetStars(1);
            else if(_levelSaveDataProxy.Score < _levelConfig.ScoreForThreeStars)
                _view.SetStars(2);
            else
                _view.SetStars(3);
        }
    }
}
