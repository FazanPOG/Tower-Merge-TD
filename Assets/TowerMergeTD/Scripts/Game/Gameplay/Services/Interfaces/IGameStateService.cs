namespace TowerMergeTD.Game.Gameplay
{
    public interface IGameStateService
    {
        void Register(IGameStateHandler handler);
        void Unregister(IGameStateHandler handler);
        void ChangeState(IGameState gameState);
    }
}