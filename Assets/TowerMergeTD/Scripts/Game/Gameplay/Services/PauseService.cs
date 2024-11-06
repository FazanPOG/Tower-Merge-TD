using System.Collections.Generic;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
        
        public bool IsPaused { get; private set; }
        
        public void Register(IPauseHandler handler) => _pauseHandlers.Add(handler);
        public void Unregister(IPauseHandler handler) => _pauseHandlers.Remove(handler);
        
        public void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
            
            Time.timeScale = isPaused ? 0f : 1f;
            
            foreach (var pauseHandler in _pauseHandlers)
                pauseHandler.HandlePause(IsPaused);
        }
    }
}