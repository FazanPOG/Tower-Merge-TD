namespace TowerMergeTD.Game.Gameplay
{
    public class RewardCalculatorService : IRewardCalculatorService
    {
        //TODO: GameTimerService
        public RewardCalculatorService()
        {
            
        }
        
        public int CalculateGoldReward()
        {
            return 100;
        }

        public int CalculateGemReward()
        {
            return 5;
        }
    }
}