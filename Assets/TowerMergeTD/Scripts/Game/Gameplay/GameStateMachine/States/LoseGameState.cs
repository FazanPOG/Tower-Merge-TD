using GamePush;

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
            GP_Game.GameplayStop();
            
            _pauseService.SetPause(true);
        }

        public void Exit() { }
    }
}