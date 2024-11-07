namespace TowerMergeTD.Game.Gameplay
{
    public class LoseGameState : IGameState
    {
        private readonly IPauseService _pauseService;

        public LoseGameState(IPauseService pauseService)
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