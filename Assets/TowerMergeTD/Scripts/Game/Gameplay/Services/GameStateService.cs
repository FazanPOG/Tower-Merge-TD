using System.Collections.Generic;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameStateService : IGameStateService
    {
        private readonly List<IGameStateHandler> _handlers = new List<IGameStateHandler>();
        
        public void Register(IGameStateHandler handler)
        {
            _handlers.Add(handler);
        }

        public void Unregister(IGameStateHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void ChangeState(IGameState gameState)
        {
            foreach (var handler in _handlers)
                handler.HandleGameState(gameState);
        }
    }
}