using System;
using TowerMergeTD.GameRoot;
using YG;

namespace TowerMergeTD.API
{
    public class YandexGamesADService : IADService
    {
        private readonly ProjectConfig _projectConfig;
        public bool IsFullscreenAvailable => true;
        public bool IsRewardedAvailable => true;
        
        public event Action<bool> OnFullscreenClose;
        public event Action<string> OnRewardedReward;

        private string _currentRewardID;

        public YandexGamesADService(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
            
            YandexGame.RewardVideoEvent += RewardVideoEvent;
            YandexGame.CloseFullAdEvent += () => { OnFullscreenClose?.Invoke(true); };
        }

        private void RewardVideoEvent(int _)
        {
            if(string.IsNullOrEmpty(_currentRewardID))
                throw new NullReferenceException($"Missing AD reward");
            
            OnRewardedReward?.Invoke(_currentRewardID);
            _currentRewardID = String.Empty;
        }

        public void ShowFullscreen()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowRewarded(string rewardID)
        {
            _currentRewardID = rewardID;
            YandexGame.RewVideoShow(0);
        }
    }
}