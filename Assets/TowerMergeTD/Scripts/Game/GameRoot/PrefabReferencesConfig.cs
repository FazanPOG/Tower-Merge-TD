using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/PrefabReferences", order = 1)]
    public class PrefabReferencesConfig : ScriptableObject
    {
        [Header("Towers")]
        [SerializeField] private TowerObject[] _towersPrefab;
        [Header("Towers additional")]
        [SerializeField] private Rocket _smallRocket;
        [SerializeField] private Rocket _bigRocket;
        [SerializeField] private Lightning _lightning;
        [Header("Enemy")]
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject[] TowersPrefab => _towersPrefab;
        
        public Rocket SmallRocket => _smallRocket;

        public Rocket BigRocket => _bigRocket;

        public Lightning Lightning => _lightning;
        
        public Enemy EnemyPrefab => enemyPrefab;
    }
}