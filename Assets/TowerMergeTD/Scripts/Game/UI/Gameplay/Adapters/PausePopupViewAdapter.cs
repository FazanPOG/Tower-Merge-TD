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
        private readonly int _currentLevelNumber;
        private readonly ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private readonly IPauseService _pauseService;

        public PausePopupViewAdapter(PausePopupView view, int currentLevelNumber, Button pauseButton, ReactiveProperty<SceneEnterParams> exitSceneSignalBus, IPauseService pauseService)
        {
            _view = view;
            _currentLevelNumber = currentLevelNumber;
            _exitSceneSignalBus = exitSceneSignalBus;
            _pauseService = pauseService;
            
            pauseButton.onClick.AddListener(HandlePauseButtonClicked);
            _view.OnContinueButtonClicked += HandleContinueButtonClicked;
            _view.OnRestartButtonClicked += HandleRestartButtonClicked;
            _view.OnExitButtonClicked += HandleExitButtonClicked;
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
            _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelNumber));
        }

        private void HandleExitButtonClicked()
        {
            _exitSceneSignalBus.OnNext(new MainMenuEnterParams("Result"));
        }
    }
}