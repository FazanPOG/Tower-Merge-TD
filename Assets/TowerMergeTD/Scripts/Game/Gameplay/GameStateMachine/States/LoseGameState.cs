using TowerMergeTD.API;

namespace TowerMergeTD.Game.Gameplay
{
    public class LoseGameState : IGameState
    {
        private readonly IPauseService _pauseService;
        private readonly IAPIEnvironmentService _apiEnvironmentService;

        public LoseGameState(IPauseService pauseService, IAPIEnvironmentService apiEnvironmentService)
        {
            _pauseService = pauseService;
            _apiEnvironmentService = apiEnvironmentService;
        }
        
        public void Enter()
        {
            _apiEnvironmentService.GameplayStop();
            
            _pauseService.SetPause(true);
        }

        public void Exit() { }
    }
}