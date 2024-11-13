using TowerMergeTD.Game.State;

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

        public WinGameState(
            int currentLevelIndex, 
            IPauseService pauseService, 
            IScoreService scoreService, 
            IRewardCalculatorService rewardCalculatorService,
            IGameStateProvider gameStateProvider,
            ICurrencyProvider currencyProvider)
        {
            _currentLevelIndex = currentLevelIndex;
            _pauseService = pauseService;
            _scoreService = scoreService;
            _rewardCalculatorService = rewardCalculatorService;
            _gameStateProvider = gameStateProvider;
            _currencyProvider = currencyProvider;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(true);
            _scoreService.CalculateScore();
            
            SaveLevelScore();
            SaveReward();
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
        
        public void Exit() { }
    }
}