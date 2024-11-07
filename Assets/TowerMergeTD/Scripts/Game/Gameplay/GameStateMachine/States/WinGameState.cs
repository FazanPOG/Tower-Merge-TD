namespace TowerMergeTD.Game.Gameplay
{
    public class WinGameState : IGameState
    {
        private readonly IPauseService _pauseService;

        public WinGameState(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(true);
        }

        public void Exit() { }
    }
}