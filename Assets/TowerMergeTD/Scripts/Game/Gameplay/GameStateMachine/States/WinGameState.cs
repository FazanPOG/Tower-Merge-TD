using TowerMergeTD.API;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class WinGameState : IGameState
    {
        private readonly int _currentLevelIndex;
        private readonly IPauseService _pauseService;
        private readonly IScoreService _scoreService;
        private readonly IRewardCalculatorService _rewardCalculatorService;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICurrencyProvider _currencyProvider;
        private readonly IAPIEnvironmentService _apiEnvironmentService;

        public WinGameState(
            int currentLevelIndex, 
            IPauseService pauseService, 
            IScoreService scoreService, 
            IRewardCalculatorService rewardCalculatorService,
            IGameStateProvider gameStateProvider,
            ICurrencyProvider currencyProvider,
            IAPIEnvironmentService apiEnvironmentService)
        {
            _currentLevelIndex = currentLevelIndex;
            _pauseService = pauseService;
            _scoreService = scoreService;
            _rewardCalculatorService = rewardCalculatorService;
            _gameStateProvider = gameStateProvider;
            _currencyProvider = currencyProvider;
            _apiEnvironmentService = apiEnvironmentService;
        }
        
        public void Enter()
        {
            _apiEnvironmentService.GameplayStop();
            
            _pauseService.SetPause(true);
            _scoreService.CalculateScore();
            
            SaveLevelScore();
            SaveReward();
            UnlockNextLevel();
        }

        private void SaveLevelScore()
        {
            var levelSavedData = _gameStateProvider.GameState.LevelDatas[_currentLevelIndex];
            
            int levelSavedScore = levelSavedData.Score;
            int newScore = _scoreService.Score;

            if (newScore > levelSavedScore)
            {
                _gameStateProvider.GameState.LevelDatas.Remove(levelSavedData);
                LevelSaveData newSave = new LevelSaveData()
                {
                    ID = _currentLevelIndex,
                    IsOpen = true,
                    Score = newScore
                };
                
                _gameStateProvider.GameState.LevelDatas.Add(new LevelSaveDataProxy(newSave));
                _gameStateProvider.SaveGameState();
            }
        }

        private void SaveReward()
        {
            _currencyProvider.Coins.Coins.Value += _rewardCalculatorService.CalculateCoinReward();
            _currencyProvider.Gems.Gems.Value += _rewardCalculatorService.CalculateGemReward();
            
            _currencyProvider.SaveCoins();
            _currencyProvider.SaveGems();
        }

        private void UnlockNextLevel()
        {
            int nextLevelIndex = _currentLevelIndex + 1;
            
            if(nextLevelIndex + 1 >= _gameStateProvider.GameState.LevelDatas.Count)
                return;
            
            LevelSaveData newNextLevelData = new LevelSaveData()
            {
                ID = nextLevelIndex,
                IsOpen = true,
                Score = 0,
            };

            var previousNextLevelSaveData = _gameStateProvider.GameState.LevelDatas[_currentLevelIndex];
            _gameStateProvider.GameState.LevelDatas.Remove(previousNextLevelSaveData);
            _gameStateProvider.GameState.LevelDatas.Add(new LevelSaveDataProxy(newNextLevelData));
            _gameStateProvider.SaveGameState();
        }
        
        public void Exit() { }
    }
}