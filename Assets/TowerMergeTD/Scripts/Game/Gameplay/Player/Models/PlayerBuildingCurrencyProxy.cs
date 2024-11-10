using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public class PlayerBuildingCurrencyProxy
    {
        public ReactiveProperty<int> BuildingCurrency = new ReactiveProperty<int>();
        
        public PlayerBuildingCurrencyProxy(PlayerBuildingCurrency playerBuildingCurrency)
        {
            BuildingCurrency.Value = playerBuildingCurrency.Value;

            BuildingCurrency.Subscribe(newValue => playerBuildingCurrency.Value = newValue);
        }
    }
}