using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Tilemap _baseTileMap;
        [SerializeField] private Tilemap _environmentTileMap;
        [SerializeField] private Transform _towersParent;
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private Transform[] _pathPoints;
        [SerializeField] private Transform _enemySpawnPosition;
        [SerializeField] private EnemyFinishTrigger _enemyFinish;

        public LevelConfig LevelConfig => _levelConfig;
        public Tilemap BaseTileMap => _baseTileMap;
        public Tilemap EnvironmentTileMap => _environmentTileMap;
        public Transform TowersParent => _towersParent;
        public Transform EnemiesParent => _enemiesParent;
        public Transform[] PathPoints => _pathPoints;
        public Transform EnemySpawnPosition => _enemySpawnPosition;
        public EnemyFinishTrigger EnemyFinish => _enemyFinish;
    }
}
