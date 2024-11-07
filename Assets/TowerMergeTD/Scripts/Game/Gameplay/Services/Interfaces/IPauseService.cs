using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IPauseService
    {
        Observable<bool> IsPaused { get; }
        void Register(IPauseHandler handler);
        void Unregister(IPauseHandler handler);
        void SetPause(bool isPaused);
    }
}