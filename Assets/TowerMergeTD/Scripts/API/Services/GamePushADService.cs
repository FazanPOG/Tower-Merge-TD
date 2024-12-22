using System;
//using GamePush;
using TowerMergeTD.GameRoot;

namespace TowerMergeTD.API
{
    public class GamePushADService : IADService
    {
        private readonly ProjectConfig _projectConfig;

        public bool IsFullscreenAvailable => _projectConfig.IsDevelopmentSettings; //|| GP_Ads.IsFullscreenAvailable();
        public bool IsRewardedAvailable => _projectConfig.IsDevelopmentSettings; //|| GP_Ads.IsRewardedAvailable();

        public event Action<bool> OnFullscreenClose;
        public event Action<string> OnRewardedReward;

        public GamePushADService(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
            
            //GP_Ads.ShowSticky();
        }

        public void ShowFullscreen()
        {
            //GP_Ads.ShowFullscreen(null, OnFullscreenClose);
        }

        public void ShowRewarded(string rewardID)
        {
            //GP_Ads.ShowRewarded(rewardID, OnRewardedReward);
        }
    }
}