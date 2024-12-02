using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
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
        private readonly bool _isDevelopment;
        private readonly IADService _adService;

        public PausePopupViewAdapter
            (
            PausePopupView view, 
            int currentLevelIndex, 
            Button pauseButton, 
            ReactiveProperty<SceneEnterParams> exitSceneSignalBus, 
            IPauseService pauseService,
            ILocalizationAsset localizationAsset,
            bool isDevelopment,
            IADService adService
            )
        {
            _view = view;
            _currentLevelIndex = currentLevelIndex;
            _pauseButton = pauseButton;
            _exitSceneSignalBus = exitSceneSignalBus;
            _pauseService = pauseService;
            _isDevelopment = isDevelopment;
            _adService = adService;

            _view.SetPauseText(localizationAsset.GetTranslation(LocalizationKeys.PAUSE_KEY));
            _view.SetContinueText(localizationAsset.GetTranslation(LocalizationKeys.CONTINUE_KEY));
            _view.SetRestartText(localizationAsset.GetTranslation(LocalizationKeys.RESTART_KEY));
            _view.SetExitText(localizationAsset.GetTranslation(LocalizationKeys.EXIT_KEY));
            
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
            if (_adService.IsFullscreenAvailable)
            {
                if(_isDevelopment == false)
                    _adService.OnFullscreenClose += (success) => _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
                else
                    _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
            }
            else
            {
                _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
            }
        }

        private void HandleExitButtonClicked()
        {
            _exitSceneSignalBus.OnNext(new MainMenuEnterParams("Result"));
        }
    }
}