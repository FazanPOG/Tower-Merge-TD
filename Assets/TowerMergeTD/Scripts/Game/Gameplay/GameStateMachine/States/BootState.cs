using GamePush;

namespace TowerMergeTD.Game.Gameplay
{
    public class BootState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelConfig _levelConfig;

        public BootState(GameStateMachine gameStateMachine, LevelConfig levelConfig)
        {
            _gameStateMachine = gameStateMachine;
            _levelConfig = levelConfig;
        }
        
        public void Enter()
        {
            GP_Game.GameplayStart();
            
            if(_levelConfig.IsTutorial)
                _gameStateMachine.EnterIn<TutorialState>();
            else
                _gameStateMachine.EnterIn<GameplayState>();
        }
        
        public void Exit() { }
    }
}