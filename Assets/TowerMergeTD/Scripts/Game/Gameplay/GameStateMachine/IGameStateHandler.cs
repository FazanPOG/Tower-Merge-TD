using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IGameStateHandler
    {
        void HandleGameState(IGameState gameState);
    }
}