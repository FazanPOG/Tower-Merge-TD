using R3;

namespace TowerMergeTD.Game.State
{
    public class PlayerMoneyProxy
    {
        public ReactiveProperty<int> Money = new ReactiveProperty<int>();

        public PlayerMoneyProxy(PlayerMoney playerMoney)
        {
            Money.Value = playerMoney.Value;

            Money.Subscribe(newValue => playerMoney.Value = newValue);
        }
    }
}