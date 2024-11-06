using System;
using R3;
using TowerMergeTD.Game.Gameplay;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PausePopupViewAdapter
    {
        private readonly PausePopupView _view;
        private readonly Subject<Unit> _exitSceneSignalBus;
        private readonly IPauseService _pauseService;

        public PausePopupViewAdapter(PausePopupView view, Button pauseButton, Subject<Unit> exitSceneSignalBus, IPauseService pauseService)
        {
            _view = view;
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
            
        }

        private void HandleExitButtonClicked()
        {
            _exitSceneSignalBus.OnNext(Unit.Default);
        }
    }
}