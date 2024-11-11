using System;
using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IGameTimerService
    {
        ReadOnlyReactiveProperty<TimeSpan> Time { get; }
        void StartTimer();
        void StopTimer();
    }
}