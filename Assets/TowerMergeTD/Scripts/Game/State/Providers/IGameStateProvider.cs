using R3;
using TowerMergeTD.Scripts.Game.State.Root;

namespace TowerMergeTD.Scripts.Game.State
{
    public interface IGameStateProvider
    {
        GameStateProxy GameState { get; }
        Observable<GameStateProxy> LoadGameState();
        Observable<bool> SaveGameState();
        Observable<bool> ResetGameState();
    }
}