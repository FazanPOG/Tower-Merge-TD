using System;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class RewardCalculatorService : IRewardCalculatorService
    {
        private readonly IScoreService _scoreService;
        private readonly int _currentLevelIndex;
        private readonly LevelConfig _levelConfig;
        private readonly IGameStateProvider _gameStateProvider;

        public RewardCalculatorService(
            IScoreService scoreService, 
            int currentLevelIndex, 
            LevelConfig levelConfig, 
            IGameStateProvider gameStateProvider)
        {
            _scoreService = scoreService;
            _currentLevelIndex = currentLevelIndex;
            _levelConfig = levelConfig;
            _gameStateProvider = gameStateProvider;
        }
        
        //TODO
        public int CalculateGoldReward()
        {
            return 100;
        }

        public int CalculateGemReward()
        {
            int newStarsCount = CalculateNewStarsEarned();
            return newStarsCount;
        }

        private int CalculateNewStarsEarned()
        {
            int newScore = _scoreService.Score;
            int savedScore = _gameStateProvider.GameState.LevelDatas[_currentLevelIndex].Score;
            
            if (newScore < _levelConfig.ScoreForOneStar)
                throw new ArgumentException($"Win game, but score is less than required for 1 star: ({_levelConfig.ScoreForOneStar})");

            if (newScore <= savedScore)
                return 0;

            int newStars = 0;
            int savedStars = 0;

            if (newScore >= _levelConfig.ScoreForThreeStars)
                newStars = 3;
            else if (newScore >= _levelConfig.ScoreForTwoStars)
                newStars = 2;
            else if (newScore >= _levelConfig.ScoreForOneStar)
                newStars = 1;

            if (savedScore >= _levelConfig.ScoreForThreeStars)
                savedStars = 3;
            else if (savedScore >= _levelConfig.ScoreForTwoStars)
                savedStars = 2;
            else if (savedScore >= _levelConfig.ScoreForOneStar)
                savedStars = 1;

            int different = newStars - savedStars;
            return Math.Clamp(different, 0, 3);
        }
    }
}