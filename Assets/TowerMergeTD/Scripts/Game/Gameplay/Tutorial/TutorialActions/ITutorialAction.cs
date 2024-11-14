using System;
using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface ITutorialAction : IDisposable
    {
        ReadOnlyReactiveProperty<bool> IsComplete { get; }
        void StartAction();
    }
}