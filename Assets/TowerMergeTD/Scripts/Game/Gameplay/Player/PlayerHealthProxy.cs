using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public class PlayerHealthProxy
    {
        public ReactiveProperty<int> Health = new ReactiveProperty<int>();
        
        public PlayerHealthProxy(PlayerHealth playerHealth)
        {
            Health.Value = playerHealth.Value;

            Health.Subscribe(newValue => playerHealth.Value = newValue);
        }
    }
}