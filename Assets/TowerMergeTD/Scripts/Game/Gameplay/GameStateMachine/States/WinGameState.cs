using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    //TODO
    public class WinGameState : IGameState
    {
        private readonly IPauseService _pauseService;

        public WinGameState(IPauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void Enter()
        {
            Debug.Log("Win game");
            _pauseService.SetPause(true);
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}