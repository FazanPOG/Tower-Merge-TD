using System;
using System.Collections;
using TowerMergeTD.Utils;
using UnityEngine;
using YG;

namespace TowerMergeTD.API
{
    public class YandexGamesADService : IADService
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private bool _isFullscreenAvailable;

        public bool IsFullscreenAvailable => true;
        public bool IsRewardedAvailable => true;
        
        public event Action<bool> OnFullscreenClose;
        public event Action<string> OnRewardedReward;

        private string _currentRewardID;

        public YandexGamesADService(MonoBehaviourWrapper monoBehaviourWrapper)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            
            _isFullscreenAvailable = false;
            //_monoBehaviourWrapper.StartCoroutine(FullScreenAdCooldown());
            
            YandexGame.RewardVideoEvent += RewardVideoEvent;
            YandexGame.CloseFullAdEvent += OnFullscreenCloseInvoke;
        }

        private void RewardVideoEvent(int _)
        {
            if(string.IsNullOrEmpty(_currentRewardID))
                throw new NullReferenceException($"Missing AD reward");
            
            OnRewardedReward?.Invoke(_currentRewardID);
            _currentRewardID = String.Empty;
        }

        private void OnFullscreenCloseInvoke() => OnFullscreenClose?.Invoke(true);

        public void ShowFullscreen()
        {
            YandexGame.FullscreenShow();
            _isFullscreenAvailable = false;
            //_monoBehaviourWrapper.StartCoroutine(FullScreenAdCooldown());
        }

        public void ShowRewarded(string rewardID)
        {
            _currentRewardID = rewardID;
            YandexGame.RewVideoShow(0);
        }

        private IEnumerator FullScreenAdCooldown()
        {
            Debug.Log("FullScreenAdCooldown");
            //yield return new WaitForSecondsRealtime(YandexGame.Instance.infoYG.fullscreenAdInterval);
            yield return new WaitForSecondsRealtime(5f);
            Debug.Log("CAN SHOW FULLSCREEN AD");
            _isFullscreenAvailable = true;
        }
    }
}