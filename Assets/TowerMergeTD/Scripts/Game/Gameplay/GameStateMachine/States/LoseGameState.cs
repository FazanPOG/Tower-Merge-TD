using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    //TODO
    public class LoseGameState : IGameState
    {
        private readonly IPauseService _pauseService;

        public LoseGameState(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void Enter()
        {
            Debug.Log("Lose game");
            _pauseService.SetPause(true);
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}