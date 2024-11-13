using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public interface ITutorialAction
    {
        ReadOnlyReactiveProperty<bool> IsComplete { get; }
        void StartAction();
    }
}