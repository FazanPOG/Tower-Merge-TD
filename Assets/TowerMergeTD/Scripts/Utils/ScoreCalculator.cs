using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerMergeTD.Utils
{
    public class ScoreCalculator : MonoBehaviour
    {
        private readonly float _healthWeight = 2.0f;
        private readonly float _enemyWeight = 3.0f;
        private readonly float _materialWeight = 1.5f;
        
        [Button("Calculate score for every stars")]
        public void CalculateScore(int initialHealth, int totalEnemies, int totalMaterials)
        {
            int maxScore = Mathf.RoundToInt(
                (initialHealth * _healthWeight) + 
                (totalEnemies * _enemyWeight) + 
                (totalMaterials * _materialWeight)
            );

            int oneStarScore = Mathf.RoundToInt(maxScore * 0.3f / 100) * 100;
            int twoStarScore = Mathf.RoundToInt(maxScore * 0.6f / 100) * 100;
            int threeStarScore = Mathf.RoundToInt(maxScore / 100) * 100;
            
            UnityEngine.Debug.Log($"One star score: {oneStarScore * 10}");
            UnityEngine.Debug.Log($"Two stars score: {twoStarScore * 10}");
            UnityEngine.Debug.Log($"Three stars score: {threeStarScore * 10}");
        }
    }
}