using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerGemsViewAdapter
    {
        private readonly PlayerGemsView _view;
        private readonly ShopPopupView _shopView;
        private readonly ShopPopupViewAdapter _shopAdapter;

        public PlayerGemsViewAdapter(PlayerGemsView view, PlayerGemsProxy gemsProxy, ShopPopupView shopView, ShopPopupViewAdapter shopAdapter)
        {
            _view = view;
            _shopView = shopView;
            _shopAdapter = shopAdapter;

            _view.OnAddButtonClicked += OnAddButtonClicked;
            gemsProxy.Gems.Subscribe(UpdateView);
        }

        private void OnAddButtonClicked()
        {
            _shopView.Show();
            _shopAdapter.ShowGemShop();
        }

        private void UpdateView(int gems)
        {
            _view.SetGemsValueText(gems.ToString("N0"));
        }
    }
}