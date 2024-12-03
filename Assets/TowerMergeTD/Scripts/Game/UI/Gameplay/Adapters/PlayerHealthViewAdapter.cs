using System;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerHealthViewAdapter
    {
        private const int AD_REWARD_VALUE = 10;
        private const string REWARDED_KEY = "Health";
        
        private readonly PlayerHealthView _view;
        private readonly PlayerHealthProxy _playerHealthProxy;
        private readonly AdConfirmationPopupView _confirmationPopup;
        private readonly ILocalizationAsset _localizationAsset;
        private readonly IADService _adService;
        private readonly IPauseService _pauseService;

        public PlayerHealthViewAdapter(
            PlayerHealthView view, 
            PlayerHealthProxy playerHealthProxy, 
            AdConfirmationPopupView confirmationPopup,
            ILocalizationAsset localizationAsset,
            IADService adService,
            IPauseService pauseService)
        {
            _view = view;
            _playerHealthProxy = playerHealthProxy;
            _confirmationPopup = confirmationPopup;
            _localizationAsset = localizationAsset;
            _adService = adService;
            _pauseService = pauseService;

            _playerHealthProxy.Health.Subscribe(UpdateView);
            _view.OnPlusButtonClicked += OnPlusButtonClicked;
            _adService.OnRewardedReward += OnRewardedReward;
        }

        private void OnRewardedReward(string rewardKey)
        {
            if (rewardKey == REWARDED_KEY)
                _playerHealthProxy.Health.Value += AD_REWARD_VALUE;

            CloseConfirmationPopup();
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
            _confirmationPopup.SetHealthSprite();
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
        
        private void UpdateView(int health)
        {
            _view.UpdateText($"{Math.Max(0, health)}");
        }
    }
}