namespace TowerMergeTD.Game.Gameplay
{
    public interface IRewardCalculatorService
    {
        int CalculateGoldReward();
        int CalculateGemReward();
    }
}