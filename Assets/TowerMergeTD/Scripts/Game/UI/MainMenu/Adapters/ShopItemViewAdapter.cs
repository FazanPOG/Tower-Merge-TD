using System;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.MainMenu;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    //TODO
    public class ShopItemViewAdapter
    {
        private readonly ShopItemView _view;
        private readonly ShopItemConfig _itemConfig;
        private readonly PlayerCoinsProxy _playerCoins;
        private readonly PlayerGemsProxy _playerGems;
        private readonly ILocalizationAsset _localizationAsset;

        public ShopItemViewAdapter(ShopItemView view, PlayerCoinsProxy playerCoins, PlayerGemsProxy playerGems, ILocalizationAsset localizationAsset)
        {
            _view = view;
            _itemConfig = _view.ShopItemConfig;
            _playerCoins = playerCoins;
            _playerGems = playerGems;
            _localizationAsset = localizationAsset;

            UpdateView();
            
            _view.OnBuyButtonClicked += OnBuyButtonClicked;
        }

        private void OnBuyButtonClicked()
        {
            if (CanBuy())
                Buy();
            else
                RedirectToCurrencyPurchase();
        }

        private void Buy()
        {
            
        }

        private void RedirectToCurrencyPurchase()
        {
            throw new NotImplementedException();
        }

        private bool CanBuy()
        {
            return true;
        }

        private void UpdateView()
        {
            _view.SetItemSprite(_itemConfig.ItemIcon);
            
            switch (_itemConfig.ItemPriceType)
            {
                case ShopItemPriceType.Coin:
                    _view.SetCurrencyImageActiveState(true);
                    _view.SetCurrencySprite(_itemConfig.CurrencyIcon);
                    _view.SetPriceText(_itemConfig.ItemPrice.ToString());
                    break;
                
                case ShopItemPriceType.Gem:
                    _view.SetCurrencyImageActiveState(true);
                    _view.SetCurrencySprite(_itemConfig.CurrencyIcon);
                    _view.SetPriceText(_itemConfig.ItemPrice.ToString());
                    break;
                
                case ShopItemPriceType.FREE:
                    _view.SetCurrencyImageActiveState(false);
                    _view.SetPriceText(_localizationAsset.GetTranslation(LocalizationKeys.FREE_KEY));
                    break;
                
                case ShopItemPriceType.AD:
                    _view.SetPriceTextActiveState(false);
                    _view.SetCurrencyAdSprite();
                    break;
            }

            switch (_itemConfig.ItemType)
            {
                case ShopItemType.Coin:
                    _view.SetValueText(_itemConfig.CurrencyValue.ToString());
                    break;
                
                case ShopItemType.Gem:
                    _view.SetValueText(_itemConfig.CurrencyValue.ToString());
                    break;
                
                case ShopItemType.Tower:
                    string text = GetTowerLocalizedText(_itemConfig.TowerToOpen);
                    _view.SetValueText(text);
                    break;
            }
        }

        private string GetTowerLocalizedText(TowerType towerType)
        {
            string key = $"{towerType.ToString().ToUpper()}_KEY";
            return _localizationAsset.GetTranslation(key);
        }
    }
}