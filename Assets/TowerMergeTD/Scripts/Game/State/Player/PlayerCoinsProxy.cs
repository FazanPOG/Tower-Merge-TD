using R3;

namespace TowerMergeTD.Game.State
{
    public class PlayerCoinsProxy
    {
        public ReactiveProperty<int> Coins = new ReactiveProperty<int>();

        public PlayerCoinsProxy(PlayerCoins playerCoins)
        {
            Coins.Value = playerCoins.Value;

            Coins.Subscribe(newValue => playerCoins.Value = newValue);
        }
    }
}