using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/PrefabReferences", order = 1)]
    public class PrefabReferencesConfig : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private TowerObject _towerPrefab;
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject TowerPrefab => _towerPrefab;

        public Enemy EnemyPrefab => enemyPrefab;
    }
}