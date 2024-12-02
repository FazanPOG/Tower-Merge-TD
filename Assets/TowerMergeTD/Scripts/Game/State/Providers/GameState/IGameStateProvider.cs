using R3;

namespace TowerMergeTD.Game.State
{
    public interface IGameStateProvider
    {
        GameStateProxy GameState { get; }
        Observable<GameStateProxy> LoadGameState();
        Observable<bool> SaveGameState();
        Observable<bool> ResetGameState();
    }
}