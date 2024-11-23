using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class ShopPopupViewAdapter
    {
        private readonly ShopPopupView _shopPopupView;
        private readonly ShopTowersView _shopTowersView;
        private readonly ShopCoinView _shopCoinView;
        private readonly ShopGemView _shopGemView;

        public ShopPopupViewAdapter(
            ShopPopupView shopPopupView, 
            ShopTowersView shopTowersView,
            ShopCoinView shopCoinView,
            ShopGemView shopGemView,
            ILocalizationAsset localizationAsset)
        {
            _shopPopupView = shopPopupView;
            _shopTowersView = shopTowersView;
            _shopCoinView = shopCoinView;
            _shopGemView = shopGemView;

            _shopPopupView.SetShopText(localizationAsset.GetTranslation(LocalizationKeys.SHOP_KEY));
            _shopPopupView.SetTowerText(localizationAsset.GetTranslation(LocalizationKeys.TOWER_KEY));
            _shopPopupView.SetCoinText(localizationAsset.GetTranslation(LocalizationKeys.COIN_KEY));
            _shopPopupView.SetGemText(localizationAsset.GetTranslation(LocalizationKeys.GEM_KEY));
            
            Subscribe();
            ShowTowersShop();
        }

        private void Subscribe()
        {
            _shopPopupView.OnCloseButtonClicked += Close;
            _shopPopupView.OnTowerShopButtonClicked += ShowTowersShop;
            _shopPopupView.OnCoinShopButtonClicked += ShowCoinShop;
            _shopPopupView.OnGemShopButtonClicked += ShowGemShop;
        }

        private void Close()
        {
            _shopPopupView.Hide();
        }

        private void ShowTowersShop()
        {
            _shopTowersView.Show();
            _shopCoinView.Hide();
            _shopGemView.Hide();
            
            _shopPopupView.SetActiveTowerView(true);
            _shopPopupView.SetActiveCoinView(false);
            _shopPopupView.SetActiveGemView(false);
        }

        public void ShowCoinShop()
        {
            _shopCoinView.Show();
            _shopTowersView.Hide();
            _shopGemView.Hide();

            _shopPopupView.SetActiveCoinView(true);
            _shopPopupView.SetActiveTowerView(false);
            _shopPopupView.SetActiveGemView(false);
        }

        public void ShowGemShop()
        {
            _shopGemView.Show();
            _shopTowersView.Hide();
            _shopCoinView.Hide();

            _shopPopupView.SetActiveGemView(true);
            _shopPopupView.SetActiveCoinView(false);
            _shopPopupView.SetActiveTowerView(false);
        }
    }
}