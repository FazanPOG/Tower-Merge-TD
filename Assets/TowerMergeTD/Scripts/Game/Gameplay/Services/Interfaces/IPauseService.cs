namespace TowerMergeTD.Game.Gameplay
{
    public interface IPauseService
    {
        bool IsPaused { get; }
        void Register(IPauseHandler handler);
        void Unregister(IPauseHandler handler);
        void SetPause(bool isPaused);
    }
}