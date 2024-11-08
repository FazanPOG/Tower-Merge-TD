using R3;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.UI
{
    public class PlayerBuildingCurrencyViewAdapter
    {
        private readonly PlayerBuildingCurrencyView _view;

        public PlayerBuildingCurrencyViewAdapter(PlayerBuildingCurrencyView view, PlayerBuildingCurrencyProxy playerBuildingCurrencyProxy)
        {
            _view = view;

            playerBuildingCurrencyProxy.BuildingCurrency.Subscribe(UpdateView);
        }

        private void UpdateView(int value)
        {
            _view.SetValueText($"{value}");
        }
    }
}