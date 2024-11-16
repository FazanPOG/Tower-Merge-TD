using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/PrefabReferences", order = 1)]
    public class PrefabReferencesConfig : ScriptableObject
    {
        [Header("Towers")]
        [SerializeField] private TowerObject _gunTowerPrefab;
        [SerializeField] private TowerObject _rocketTowerPrefab;
        [SerializeField] private TowerObject _laserTowerPrefab;
        [Header("Towers additional")]
        [SerializeField] private Rocket _smallRocket;
        [SerializeField] private Rocket _bigRocket;
        [Header("Enemy")]
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject GunTowerPrefab => _gunTowerPrefab;
        public TowerObject RocketTowerPrefab => _rocketTowerPrefab;

        public TowerObject LaserTowerPrefab => _laserTowerPrefab;
        
        public Rocket SmallRocket => _smallRocket;

        public Rocket BigRocket => _bigRocket;
        
        public Enemy EnemyPrefab => enemyPrefab;
    }
}