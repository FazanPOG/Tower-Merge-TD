using System;
using GamePush;

namespace TowerMergeTD.API
{
    public class ADService : IADService
    {
        public bool IsFullscreenAvailable => GP_Ads.IsFullscreenAvailable();
        public bool IsPreloaderAvailable => GP_Ads.IsPreloaderAvailable();
        public bool IsRewardedAvailable => GP_Ads.IsRewardedAvailable();
        public bool IsStickyAvailable => GP_Ads.IsStickyAvailable();
        public bool IsFullscreenPlaying => GP_Ads.IsFullscreenPlaying();
        public bool IsPreloaderPlaying => GP_Ads.IsPreloaderPlaying();
        public bool IsRewardPlaying => GP_Ads.IsRewardPlaying();
        public bool IsStickyPlaying => GP_Ads.IsStickyPlaying();
        public bool CanShowFullscreenBeforeGamePlay => GP_Ads.CanShowFullscreenBeforeGamePlay();

        public event Action OnFullscreenStart;
        public event Action<bool> OnFullscreenClose;
        public event Action OnRewardedStart;
        public event Action<string> OnRewardedReward;
        public event Action<bool> OnRewardedClose;

        public ADService()
        {
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