using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PausePopupViewAdapter
    {
        private readonly PausePopupView _view;
        private readonly int _currentLevelIndex;
        private readonly Button _pauseButton;
        private readonly ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private readonly IPauseService _pauseService;

        public PausePopupViewAdapter
            (
            PausePopupView view, 
            int currentLevelIndex, 
            Button pauseButton, 
            ReactiveProperty<SceneEnterParams> exitSceneSignalBus, 
            IPauseService pauseService
            )
        {
            _view = view;
            _currentLevelIndex = currentLevelIndex;
            _pauseButton = pauseButton;
            _exitSceneSignalBus = exitSceneSignalBus;
            _pauseService = pauseService;

            Subscribe();
        }

        private void Subscribe()
        {
            _pauseButton.onClick.AddListener(HandlePauseButtonClicked);
            _view.OnContinueButtonClicked += HandleContinueButtonClicked;
            _view.OnRestartButtonClicked += HandleRestartButtonClicked;
            _view.OnExitButtonClicked += HandleExitButtonClicked;
            _pauseService.IsPaused.Subscribe(HandleIsPausedChanged);
        }
        
        private void HandleIsPausedChanged(bool isPaused)
        {
            _pauseButton.interactable = !isPaused;
        }
        
        private void HandlePauseButtonClicked()
        {
            _pauseService.SetPause(true);
            _view.Show();
        }

        private void HandleContinueButtonClicked()
        {
            _pauseService.SetPause(false);
            _view.Hide();
        }

        private void HandleRestartButtonClicked()
        {
            _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
        }

        private void HandleExitButtonClicked()
        {
            _exitSceneSignalBus.OnNext(new MainMenuEnterParams("Result"));
        }
    }
}