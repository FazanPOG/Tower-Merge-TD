using System;

namespace TowerMergeTD.Game.Gameplay
{
    public class ScoreService : IScoreService
    {
        private const float HEALTH_WEIGHT = 1.5f;
        private const float BUILDING_CURRENCY_WEIGHT = 1.5f;

        private readonly PlayerHealthProxy _healthProxy;
        private readonly PlayerBuildingCurrencyProxy _buildingCurrencyProxy;

        public int Score { get; private set; }

        public ScoreService(PlayerHealthProxy healthProxy, PlayerBuildingCurrencyProxy buildingCurrencyProxy)
        {
            _healthProxy = healthProxy;
            _buildingCurrencyProxy = buildingCurrencyProxy;
        }

        public void CalculateScore()
        {
            float score = 0;
            
            score += (_healthProxy.Health.CurrentValue * HEALTH_WEIGHT) + (_buildingCurrencyProxy.BuildingCurrency.CurrentValue * BUILDING_CURRENCY_WEIGHT);
            score *= 50f;
            
            Score = (int)Math.Round(score/100) * 50;
        }
    }
}