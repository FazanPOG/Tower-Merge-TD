namespace TowerMergeTD.Game.Gameplay
{
    public interface IRewardCalculatorService
    {
        int CalculateCoinReward();
        int CalculateGemReward();
    }
}