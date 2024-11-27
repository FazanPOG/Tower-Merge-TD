using System.Collections.Generic;
using GamePush;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();
        private readonly ReactiveProperty<bool> _isPaused = new ReactiveProperty<bool>();

        public Observable<bool> IsPaused => _isPaused;
        
        public void Register(IPauseHandler handler) => _pauseHandlers.Add(handler);
        public void Unregister(IPauseHandler handler) => _pauseHandlers.Remove(handler);
        
        public void SetPause(bool isPaused)
        {
            if(isPaused)
                GP_Game.Pause();
            else
                GP_Game.Resume();
            
            _isPaused.Value = isPaused;
            
            Time.timeScale = isPaused ? 0f : 1f;
            
            foreach (var pauseHandler in _pauseHandlers)
                pauseHandler.HandlePause(_isPaused.Value);
        }
    }
}