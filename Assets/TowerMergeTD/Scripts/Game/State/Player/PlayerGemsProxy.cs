using R3;

namespace TowerMergeTD.Game.State
{
    public class PlayerGemsProxy
    {
        public ReactiveProperty<int> Gems = new ReactiveProperty<int>();

        public PlayerGemsProxy(PlayerGems playerGems)
        {
            Gems.Value = playerGems.Value;

            Gems.Subscribe(newValue => playerGems.Value = newValue);
        }
    }
}