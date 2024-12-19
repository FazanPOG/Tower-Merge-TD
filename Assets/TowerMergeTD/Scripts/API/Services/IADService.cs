using System;

namespace TowerMergeTD.API
{
    public interface IADService
    {
        bool IsFullscreenAvailable { get; }
        bool IsRewardedAvailable { get; }

        event Action<bool> OnFullscreenClose;
        event Action<string> OnRewardedReward;
        
        void ShowFullscreen();
        void ShowRewarded(string rewardID);
    }
}