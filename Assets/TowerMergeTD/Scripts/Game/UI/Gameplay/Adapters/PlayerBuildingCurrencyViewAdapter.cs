using System;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerBuildingCurrencyViewAdapter : IDisposable
    {
        private const int AD_REWARD_VALUE = 25;
        private const string REWARDED_KEY = "BuildingCurrency";
        
        private readonly PlayerBuildingCurrencyView _view;
        private readonly PlayerBuildingCurrencyProxy _playerBuildingCurrencyProxy;
        private readonly AdConfirmationPopupView _confirmationPopup;
        private readonly ILocalizationAsset _localizationAsset;
        private readonly IADService _adService;
        private readonly IPauseService _pauseService;
        private readonly IDisposable _disposable;
        
        public PlayerBuildingCurrencyViewAdapter(
            PlayerBuildingCurrencyView view, 
            PlayerBuildingCurrencyProxy playerBuildingCurrencyProxy, 
            AdConfirmationPopupView confirmationPopup,
            ILocalizationAsset localizationAsset,
            IADService adService,
            IPauseService pauseService)
        {
            _view = view;
            _playerBuildingCurrencyProxy = playerBuildingCurrencyProxy;
            _confirmationPopup = confirmationPopup;
            _localizationAsset = localizationAsset;
            _adService = adService;
            _pauseService = pauseService;

            _disposable = _playerBuildingCurrencyProxy.BuildingCurrency.Subscribe(UpdateView);
            _view.OnPlusButtonClicked += OnPlusButtonClicked;
            _adService.OnRewardedReward += OnRewardedReward;
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _adService.OnRewardedReward -= OnRewardedReward;
        }
        
        private void OnRewardedReward(string rewardKey)
        {
            if (rewardKey == REWARDED_KEY)
            {
                _playerBuildingCurrencyProxy.BuildingCurrency.Value += AD_REWARD_VALUE;
                CloseConfirmationPopup();
            }
        }

        private void OnPlusButtonClicked()
        {
            _confirmationPopup.Show();
            
            UpdatePopupView(_adService.IsRewardedAvailable);
            
            _pauseService.SetPause(true);
            
            _confirmationPopup.OnConfirmButtonClicked += OnConfirmButtonClicked;
            _confirmationPopup.OnCloseButtonClicked += CloseConfirmationPopup;
        }

        private void UpdatePopupView(bool isAdAvailable)
        {
            _confirmationPopup.SetBuildingCurrencySprite();
            _confirmationPopup.SetCloseButtonText(_localizationAsset.GetTranslation(LocalizationKeys.CANCEL_KEY));
            _confirmationPopup.SetTitleText(_localizationAsset.GetTranslation(LocalizationKeys.CONFIRM_WATCH_AD_KEY));
            
            _confirmationPopup.SetActiveRewardImage(isAdAvailable);
            _confirmationPopup.SetActiveRewardText(isAdAvailable);
            _confirmationPopup.SetActiveConfirmButton(isAdAvailable);
            _confirmationPopup.SetActiveWaitAdText(!isAdAvailable);
            
            if (isAdAvailable)
            {
                _confirmationPopup.SetRewardText($"+{AD_REWARD_VALUE}");
            }
            else
            {
                _confirmationPopup.SetWaitAdText(_localizationAsset.GetTranslation(LocalizationKeys.WAIT_AD_KEY));
            }
        }
        
        private void OnConfirmButtonClicked()
        {
            _adService.ShowRewarded(REWARDED_KEY);
        }

        private void CloseConfirmationPopup()
        {
            _confirmationPopup.OnConfirmButtonClicked -= OnConfirmButtonClicked;
            _confirmationPopup.OnCloseButtonClicked -= CloseConfirmationPopup;
            
            _confirmationPopup.Hide();
            _pauseService.SetPause(false);
        }

        private void UpdateView(int value)
        {
            _view.SetValueText($"{value}");
        }
    }
}