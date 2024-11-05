namespace TowerMergeTD.Game.Gameplay
{
    public class BootState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _gameStateMachine.EnterIn<GameplayState>();
        }
        
        public void Exit() { }
    }
}