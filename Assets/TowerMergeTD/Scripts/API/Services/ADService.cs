using System;
using GamePush;
using TowerMergeTD.GameRoot;

namespace TowerMergeTD.API
{
    public class ADService : IADService
    {
        private readonly ProjectConfig _projectConfig;

        public bool IsFullscreenAvailable => _projectConfig.IsDevelopmentSettings || GP_Ads.IsFullscreenAvailable();
        public bool IsPreloaderAvailable => _projectConfig.IsDevelopmentSettings || GP_Ads.IsPreloaderAvailable();
        public bool IsRewardedAvailable => _projectConfig.IsDevelopmentSettings || GP_Ads.IsRewardedAvailable();
        public bool IsStickyAvailable => _projectConfig.IsDevelopmentSettings || GP_Ads.IsStickyAvailable();
        public bool IsFullscreenPlaying => _projectConfig.IsDevelopmentSettings || GP_Ads.IsFullscreenPlaying();
        public bool IsPreloaderPlaying => _projectConfig.IsDevelopmentSettings || GP_Ads.IsPreloaderPlaying();
        public bool IsRewardPlaying => _projectConfig.IsDevelopmentSettings || GP_Ads.IsRewardPlaying();
        public bool IsStickyPlaying => _projectConfig.IsDevelopmentSettings || GP_Ads.IsStickyPlaying();
        public bool CanShowFullscreenBeforeGamePlay => _projectConfig.IsDevelopmentSettings || GP_Ads.CanShowFullscreenBeforeGamePlay();

        public event Action OnFullscreenStart;
        public event Action<bool> OnFullscreenClose;
        public event Action OnRewardedStart;
        public event Action<string> OnRewardedReward;
        public event Action<bool> OnRewardedClose;

        public ADService(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
            
            GP_Ads.ShowSticky();
        }

        public void ShowFullscreen()
        {
            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
        }

        public void ShowRewarded(string rewardID)
        {
            GP_Ads.ShowRewarded(rewardID, OnRewardedReward, OnRewardedStart, OnRewardedClose);
        }
    }
}