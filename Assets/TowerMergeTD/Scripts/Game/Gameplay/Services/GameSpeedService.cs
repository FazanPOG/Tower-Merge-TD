using System.Collections.Generic;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameSpeedService : IGameSpeedService
    {
        private readonly ReactiveProperty<GameSpeed> _speed = new ReactiveProperty<GameSpeed>();
        private readonly List<IGameSpeedHandler> _handlers = new List<IGameSpeedHandler>();
        
        public Observable<GameSpeed> Speed => _speed;

        public void SetSpeed(GameSpeed gameSpeed)
        {
            _speed.Value = gameSpeed;

            switch (gameSpeed)
            {
                case GameSpeed.X1:
                    Time.timeScale = 1f;
                    break;
                
                case GameSpeed.X2:
                    Time.timeScale = 2f;
                    break;
                
                case GameSpeed.X4:
                    Time.timeScale = 4f;
                    break;
                
                case GameSpeed.X8:
                    Time.timeScale = 8f;
                    break;
            }
            
            foreach (var handler in _handlers)
                handler.HandleGameSpeed(_speed.CurrentValue);
        }

        public void Register(IGameSpeedHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Unregister(IGameSpeedHandler handler)
        {
            _handlers.Remove(handler);
        }
    }
}