using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IGameStateObservable
    {
        Observable<IGameState> GameState { get; }
    }
}