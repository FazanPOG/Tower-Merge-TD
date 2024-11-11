using System.Linq;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/PrefabReferences", order = 1)]
    public class PrefabReferencesConfig : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private TowerObject _towerPrefab;
        [SerializeField] private TowerObjectView[] _towerObjectViews;
        [SerializeField] private Enemy enemyPrefab;

        public TowerObject TowerPrefab => _towerPrefab;

        public Enemy EnemyPrefab => enemyPrefab;

        public TowerObjectView GetTowerViewPrefab(TowerType towerType)
        {
            string towerViewName = towerType + "TowerView";
            return _towerObjectViews.First(x => x.name == towerViewName);
        }
    }
}