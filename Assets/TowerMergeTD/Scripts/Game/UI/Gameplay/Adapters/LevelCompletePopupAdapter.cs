using System;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;

namespace TowerMergeTD.Game.UI
{
    public class LevelCompletePopupAdapter : IGameStateHandler
    {
        private readonly LevelCompletePopupView _view;
        private readonly bool _isDevelopment;
        private readonly bool _isLastLevel;
        private readonly int _currentLevelIndex;
        private readonly ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private readonly LevelConfig _levelConfig;
        private readonly IScoreService _scoreService;
        private readonly IRewardCalculatorService _rewardCalculatorService;
        private readonly IGameTimerService _gameTimerService;
        private readonly IADService _adService;
        private readonly AudioPlayer _audioPlayer;

        public LevelCompletePopupAdapter(
            LevelCompletePopupView view,
            bool isDevelopment,
            bool isLastLevel, 
            int currentLevelIndex,
            ReactiveProperty<SceneEnterParams> exitSceneSignalBus,
            LevelConfig levelConfig,
            IScoreService scoreService,
            IRewardCalculatorService rewardCalculatorService,
            IGameTimerService gameTimerService,
            ILocalizationAsset localizationAsset,
            IADService adService,
            AudioPlayer audioPlayer)
        {
            _view = view;
            _isDevelopment = isDevelopment;
            _isLastLevel = isLastLevel;
            _currentLevelIndex = currentLevelIndex;
            _exitSceneSignalBus = exitSceneSignalBus;
            _levelConfig = levelConfig;
            _scoreService = scoreService;
            _rewardCalculatorService = rewardCalculatorService;
            _gameTimerService = gameTimerService;
            _adService = adService;
            _audioPlayer = audioPlayer;

            _view.SetCompleteText(localizationAsset.GetTranslation(LocalizationKeys.COMPLETE_KEY));
            _view.SetScoreText(localizationAsset.GetTranslation(LocalizationKeys.SCORE_KEY));
            _view.SetTimeText(localizationAsset.GetTranslation(LocalizationKeys.TIME_KEY));
            _view.SetCoinText(localizationAsset.GetTranslation(LocalizationKeys.COIN_KEY));
            _view.SetGemsText(localizationAsset.GetTranslation(LocalizationKeys.GEM_KEY));
            
            Subscribe();
        }

        private void Subscribe()
        {
            _view.OnHomeButtonClicked += HandleHomeButtonClicked;
            _view.OnRestartButtonClicked += HandleRestartButtonClicked;

            if (_isLastLevel == false)
            {
                _view.ShowNextLevelButton();
                _view.OnNextLevelButtonClicked += HandleNextButtonClicked;
            }
            else
            {
                _view.HideNextLevelButton();
            }
        }

        private void HandleHomeButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _exitSceneSignalBus.OnNext(new MainMenuEnterParams("TEST"));
        } 

        private void HandleRestartButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            
            _adService.ShowFullscreen();
            _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
        }

        private void HandleNextButtonClicked()
        {
            _adService.ShowFullscreen();
            _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex + 1));
        }
        
        public void HandleGameState(IGameState gameState)
        {
            if (gameState is WinGameState)
            {
                _view.Show();
                UpdateTexts();
                SetStars();
            }
        }

        private void UpdateTexts()
        {
            _view.SetScoreValueText(_scoreService.Score.ToString("N0"));
            _view.SetTimerValueText($"{_gameTimerService.Time.CurrentValue.Minutes:D2}:{_gameTimerService.Time.CurrentValue.Seconds:D2}");
            _view.SetGoldValueText(_rewardCalculatorService.CalculateCoinReward().ToString("N0"));
            _view.SetGemsValueText(_rewardCalculatorService.CalculateGemReward().ToString("N0"));
        }

        private void SetStars()
        {
            if(_scoreService.Score < _levelConfig.ScoreForOneStar)
                throw new ArgumentException($"Win game, but score is less than required for 1 star: ({_levelConfig.ScoreForOneStar})");

            if (_scoreService.Score < _levelConfig.ScoreForTwoStars)
            {
                _audioPlayer.Play(AudioType.OneTwoStarsWin);
                _view.SetStars(1);
            }
            else if (_scoreService.Score < _levelConfig.ScoreForThreeStars)
            {
                _audioPlayer.Play(AudioType.OneTwoStarsWin);
                _view.SetStars(2);
            }
            else
            {
                _audioPlayer.Play(AudioType.ThreeStarsWin);
                _view.SetStars(3);
            }
        }
    }
}