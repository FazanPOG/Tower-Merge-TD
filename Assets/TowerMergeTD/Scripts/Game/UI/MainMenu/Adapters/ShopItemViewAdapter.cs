using System;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.MainMenu;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    //TODO
    public class ShopItemViewAdapter
    {
        private readonly ShopItemView _view;
        private readonly ShopItemConfig _itemConfig;
        private readonly PlayerCoinsProxy _playerCoins;
        private readonly PlayerGemsProxy _playerGems;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ILocalizationAsset _localizationAsset;

        public ShopItemViewAdapter(
            ShopItemView view, 
            PlayerCoinsProxy playerCoins, 
            PlayerGemsProxy playerGems,
            IGameStateProvider gameStateProvider,
            ILocalizationAsset localizationAsset)
        {
            _view = view;
            _itemConfig = _view.ShopItemConfig;
            _playerCoins = playerCoins;
            _playerGems = playerGems;
            _gameStateProvider = gameStateProvider;
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
            switch (_itemConfig.ItemPriceType)
            {
                case ShopItemPriceType.Coin:
                    _playerCoins.Coins.Value -= _itemConfig.ItemPrice;
                    break;
                
                case ShopItemPriceType.Gem:
                    _playerGems.Gems.Value -= _itemConfig.ItemPrice;
                    break;
                
                case ShopItemPriceType.AD:
                    Debug.LogError("Implement AD");
                    break;
            }
            
            switch (_itemConfig.ItemType)
            {
                case ShopItemType.Coin:
                    _playerCoins.Coins.Value += _itemConfig.CurrencyValue;
                    break;
                
                case ShopItemType.Gem:
                    _playerGems.Gems.Value += _itemConfig.CurrencyValue;
                    break;
                
                case ShopItemType.Tower:
                    _gameStateProvider.GameState.UnlockTowers.Add(_itemConfig.TowerToUnlock);
                    break;
                
                default:
                    throw new NotImplementedException($"Shop item type ({_itemConfig.ItemType}) not implemented");
            }

            if (_itemConfig.IsSinglePurchase)
            {
                _gameStateProvider.GameState.ShopPurchasedItemIDs.Add(_itemConfig.ID);
                SetSoldOutView();
            }
            
            _gameStateProvider.SaveGameState();
        }

        private void RedirectToCurrencyPurchase()
        {
            Debug.Log($"Cant buy item: {_itemConfig.ItemType}");
        }

        private bool CanBuy()
        {
            switch (_itemConfig.ItemPriceType)
            {
                case ShopItemPriceType.Coin:
                    if (_playerCoins.Coins.Value >= _itemConfig.ItemPrice)
                        return true;
                    else
                        return false;
                
                case ShopItemPriceType.Gem:
                    if (_playerGems.Gems.Value >= _itemConfig.ItemPrice)
                        return true;
                    else
                        return false;
                
                default:
                    return true;
            }
        }

        private void UpdateView()
        {
            _view.SetBuyButtonInteractable(true);
            _view.SetItemIconSprite(_itemConfig.ItemIcon);
            _view.SetItemIconBackgroundColor(_itemConfig.ItemIconBackgroundColor);
            _view.SetActiveBestLabel(_itemConfig.ShowBestLabel);
            _view.SetActiveBonusText(_itemConfig.ShowBonusText);

            if(_itemConfig.ShowBestLabel)
                _view.SetBestLabelText(_localizationAsset.GetTranslation(LocalizationKeys.BEST_KEY));
            
            if(_itemConfig.ShowBonusText)
                _view.SetBonusText($"{_localizationAsset.GetTranslation(LocalizationKeys.BONUS_KEY)} +{_itemConfig.BonusTextValue}%");
            
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
                    string text = GetTowerLocalizedText(_itemConfig.TowerToUnlock);
                    _view.SetValueText(text);
                    break;
            }
            
            if (_gameStateProvider.GameState.ShopPurchasedItemIDs.Contains(_itemConfig.ID))
                SetSoldOutView();
        }

        private void SetSoldOutView()
        {
            _view.SetBuyButtonInteractable(false);
            _view.SetCurrencyImageActiveState(false);
            _view.SetPriceTextActiveState(true);
            _view.SetPriceText(_localizationAsset.GetTranslation(LocalizationKeys.SOLD_KEY));
        }

        private string GetTowerLocalizedText(TowerType towerType)
        {
            string key = $"{towerType.ToString().ToUpper()}_KEY";
            return _localizationAsset.GetTranslation(key);
        }
    }
}