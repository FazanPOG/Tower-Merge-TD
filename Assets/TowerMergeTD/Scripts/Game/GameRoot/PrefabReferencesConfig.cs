using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/PrefabReferences", order = 1)]
    public class PrefabReferencesConfig : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private TowerObject _gunTowerPrefab;
        [SerializeField] private TowerObject _rocketTowerPrefab;
        [SerializeField] private TowerObject _noNameTowerPrefab;
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject GunTowerPrefab => _gunTowerPrefab;
        public TowerObject RocketTowerPrefab => _rocketTowerPrefab;
        public TowerObject NoNameTowerPrefab => _noNameTowerPrefab;

        public Enemy EnemyPrefab => enemyPrefab;
    }
}