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
        [SerializeField] private Rocket _smallRocket;
        [SerializeField] private Rocket _bigRocket;
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject GunTowerPrefab => _gunTowerPrefab;
        public TowerObject RocketTowerPrefab => _rocketTowerPrefab;

        public Rocket SmallRocket => _smallRocket;

        public Rocket BigRocket => _bigRocket;
        
        public Enemy EnemyPrefab => enemyPrefab;
    }
}