using System;

namespace TowerMergeTD.API
{
    public interface IADService
    {
        bool IsFullscreenAvailable { get; }
        bool IsPreloaderAvailable { get; }
        bool IsRewardedAvailable { get; }
        bool IsStickyAvailable { get; }
        bool IsFullscreenPlaying { get; }
        bool IsPreloaderPlaying { get; }
        bool IsRewardPlaying { get; }
        bool IsStickyPlaying { get; }
        bool CanShowFullscreenBeforeGamePlay { get; }

        event Action OnFullscreenStart;
        event Action<bool> OnFullscreenClose;
        event Action OnRewardedStart;
        event Action<string> OnRewardedReward;
        
        void ShowFullscreen();
        void ShowRewarded(string rewardID);
    }
}