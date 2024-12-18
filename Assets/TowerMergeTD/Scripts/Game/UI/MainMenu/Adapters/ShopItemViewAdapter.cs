using System;
using TowerMergeTD.API;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.MainMenu;
using TowerMergeTD.Game.State;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.UI
{
    public class ShopItemViewAdapter
    {
        private readonly ShopItemView _view;
        private readonly TowerInfoPopupView _towerInfoPopup;
        private readonly ShopPopupViewAdapter _shopPopupViewAdapter;
        private readonly ShopItemConfig _itemConfig;
        private readonly PlayerCoinsProxy _playerCoins;
        private readonly PlayerGemsProxy _playerGems;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly ICurrencyProvider _currencyProvider;
        private readonly ILocalizationAsset _localizationAsset;
        private readonly IADService _adService;
        private readonly AudioPlayer _audioPlayer;

        public ShopItemViewAdapter(
            ShopItemView view,
            TowerInfoPopupView towerInfoPopup,
            ShopPopupViewAdapter shopPopupViewAdapter,
            PlayerCoinsProxy playerCoins, 
            PlayerGemsProxy playerGems,
            IGameStateProvider gameStateProvider,
            ICurrencyProvider currencyProvider,
            ILocalizationAsset localizationAsset,
            IADService adService,
            AudioPlayer audioPlayer)
        {
            _view = view;
            _towerInfoPopup = towerInfoPopup;
            _shopPopupViewAdapter = shopPopupViewAdapter;
            _itemConfig = _view.ShopItemConfig;
            _playerCoins = playerCoins;
            _playerGems = playerGems;
            _gameStateProvider = gameStateProvider;
            _currencyProvider = currencyProvider;
            _localizationAsset = localizationAsset;
            _adService = adService;
            _audioPlayer = audioPlayer;

            UpdateView();
            
            _view.OnBuyButtonClicked += OnBuyButtonClicked;
            _view.OnInfoButtonClicked += OnInfoButtonClicked;
            _adService.OnRewardedReward += ADService_OnRewardedReward;
        }

        private void OnInfoButtonClicked()
        {
            _towerInfoPopup.Show();
            _towerInfoPopup.SetTitleText(GetTowerLocalizedText(_itemConfig.TowerToUnlock));
            _towerInfoPopup.SetInfoText(GetTowerInfoText(_itemConfig.TowerToUnlock));
            _audioPlayer.Play(AudioType.Button);
        }

        private void OnBuyButtonClicked()
        {
            if (CanBuy())
            {
                _audioPlayer.Play(AudioType.Cash);
                Buy();
            }
            else
            {
                RedirectToCurrencyPurchase();
            }
        }

        private void Buy()
        {
            switch (_itemConfig.ItemPriceType)
            {
                case ShopItemPriceType.Coin:
                    _playerCoins.Coins.Value -= _itemConfig.ItemPrice;
                    _currencyProvider.SaveCoins();
                    break;
                
                case ShopItemPriceType.Gem:
                    _playerGems.Gems.Value -= _itemConfig.ItemPrice;
                    _currencyProvider.SaveGems();
                    break;
                
                case ShopItemPriceType.AD:
                    _adService.ShowRewarded(_itemConfig.ID);
                    break;
            }

            if (_itemConfig.IsSinglePurchase)
            {
                _gameStateProvider.GameState.ShopPurchasedItemIDs.Add(_itemConfig.ID);
                SetSoldOutView();
            }
            
            AddReward();
        }

        private void ADService_OnRewardedReward(string rewardID)
        {
            if (rewardID == _itemConfig.ID)
            {
                AddReward();
                SetADView(_adService.IsRewardedAvailable);
            }
        }

        private void AddReward()
        {
            switch (_itemConfig.ItemType)
            {
                case ShopItemType.Coin:
                    _playerCoins.Coins.Value += _itemConfig.CurrencyValue;
                    _currencyProvider.SaveCoins();
                    break;
                
                case ShopItemType.Gem:
                    _playerGems.Gems.Value += _itemConfig.CurrencyValue;
                    _currencyProvider.SaveGems();
                    break;
                
                case ShopItemType.Tower:
                    _gameStateProvider.GameState.UnlockTowers.Add(_itemConfig.TowerToUnlock);
                    _gameStateProvider.SaveGameState();
                    break;
                
                default:
                    throw new NotImplementedException($"Shop item type ({_itemConfig.ItemType}) not implemented");
            }
        }

        private void SetADView(bool adAvailableState)
        {
            _view.SetButtonTextActiveState(!adAvailableState);
            _view.SetButtonInteractable(adAvailableState);

            if (adAvailableState)
            {
                _view.SetButtonAdSprite();
            }
            else
            {
                _view.SetButtonImageActiveState(false);
                _view.SetButtonText(_localizationAsset.GetTranslation(LocalizationKeys.WAIT_AD_KEY));
            }
        }
        
        private void RedirectToCurrencyPurchase()
        {
            switch (_itemConfig.ItemPriceType)
            {
                case ShopItemPriceType.Coin:
                    _shopPopupViewAdapter.ShowCoinShop();
                    break;
                
                case ShopItemPriceType.Gem:
                    _shopPopupViewAdapter.ShowGemShop();
                    break;
            }
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
            _view.SetButtonInteractable(true);
            _view.SetInfoButtonActiveState(false);
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
                    _view.SetButtonImageActiveState(true);
                    _view.SetButtonSprite(_itemConfig.CurrencyIcon);
                    _view.SetButtonText(_itemConfig.ItemPrice.ToString());
                    break;
                
                case ShopItemPriceType.Gem:
                    _view.SetButtonImageActiveState(true);
                    _view.SetButtonSprite(_itemConfig.CurrencyIcon);
                    _view.SetButtonText(_itemConfig.ItemPrice.ToString());
                    break;
                
                case ShopItemPriceType.FREE:
                    _view.SetButtonImageActiveState(false);
                    _view.SetButtonText(_localizationAsset.GetTranslation(LocalizationKeys.FREE_KEY));
                    break;
                
                case ShopItemPriceType.AD:
                    SetADView(_adService.IsRewardedAvailable);
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
                    _view.SetInfoButtonActiveState(true);
                    break;
            }
            
            if (_gameStateProvider.GameState.ShopPurchasedItemIDs.Contains(_itemConfig.ID))
                SetSoldOutView();
        }

        private void SetSoldOutView()
        {
            _view.SetButtonInteractable(false);
            _view.SetButtonImageActiveState(false);
            _view.SetButtonTextActiveState(true);
            _view.SetButtonText(_localizationAsset.GetTranslation(LocalizationKeys.SOLD_KEY));
        }

        private string GetTowerLocalizedText(TowerType towerType)
        {
            string key = $"{towerType.ToString().ToUpper()}_KEY";
            return _localizationAsset.GetTranslation(key);
        }
        
        private string GetTowerInfoText(TowerType towerType)
        {
            string key = $"{towerType.ToString().ToUpper()}_INFO_KEY";
            return _localizationAsset.GetTranslation(key);
        }
    }
}