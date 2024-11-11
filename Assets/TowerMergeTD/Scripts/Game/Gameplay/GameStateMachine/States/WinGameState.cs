namespace TowerMergeTD.Game.Gameplay
{
    public class WinGameState : IGameState
    {
        private readonly IPauseService _pauseService;
        private readonly IScoreService _scoreService;

        public WinGameState(IPauseService pauseService, IScoreService scoreService)
        {
            _pauseService = pauseService;
            _scoreService = scoreService;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(true);
            _scoreService.CalculateScore();
        }

        public void Exit() { }
    }
}