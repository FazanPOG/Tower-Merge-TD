using TowerMergeTD.API;

namespace TowerMergeTD.Game.Gameplay
{
    public class BootState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelConfig _levelConfig;
        private readonly IAPIEnvironmentService _apiEnvironmentService;

        public BootState(GameStateMachine gameStateMachine, LevelConfig levelConfig, IAPIEnvironmentService apiEnvironmentService)
        {
            _gameStateMachine = gameStateMachine;
            _levelConfig = levelConfig;
            _apiEnvironmentService = apiEnvironmentService;
        }
        
        public void Enter()
        {
            _apiEnvironmentService.GameplayStart();
            
            if(_levelConfig.IsTutorial)
                _gameStateMachine.EnterIn<TutorialState>();
            else
                _gameStateMachine.EnterIn<GameplayState>();
        }
        
        public void Exit() { }
    }
}