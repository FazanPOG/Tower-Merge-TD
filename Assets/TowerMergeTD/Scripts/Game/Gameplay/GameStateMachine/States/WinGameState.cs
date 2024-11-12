using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public class WinGameState : IGameState
    {
        private readonly int _currentLevelIndex;
        private readonly IPauseService _pauseService;
        private readonly IScoreService _scoreService;
        private readonly IGameStateProvider _gameStateProvider;

        public WinGameState(int currentLevelIndex, IPauseService pauseService, IScoreService scoreService, IGameStateProvider gameStateProvider)
        {
            _currentLevelIndex = currentLevelIndex;
            _pauseService = pauseService;
            _scoreService = scoreService;
            _gameStateProvider = gameStateProvider;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(true);
            _scoreService.CalculateScore();
            SaveLevelScore();
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
        
        public void Exit() { }
    }
}