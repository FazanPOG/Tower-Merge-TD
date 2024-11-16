using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IGameSpeedService
    {
        Observable<GameSpeed> Speed { get; }
        void Register(IGameSpeedHandler handler);
        void Unregister(IGameSpeedHandler handler);
        void SetSpeed(GameSpeed gameSpeed);
    }
}