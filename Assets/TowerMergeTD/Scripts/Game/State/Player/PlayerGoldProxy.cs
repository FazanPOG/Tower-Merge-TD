using R3;

namespace TowerMergeTD.Game.State
{
    public class PlayerGoldProxy
    {
        public ReactiveProperty<int> Gold = new ReactiveProperty<int>();

        public PlayerGoldProxy(PlayerGold playerGold)
        {
            Gold.Value = playerGold.Value;

            Gold.Subscribe(newValue => playerGold.Value = newValue);
        }
    }
}