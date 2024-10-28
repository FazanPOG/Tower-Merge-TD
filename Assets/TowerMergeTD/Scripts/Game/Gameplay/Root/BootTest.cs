using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.State;
using TowerMergeTD.Game.State;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class BootTest : MonoBehaviour
    {
        [SerializeField] private TowerObject _prefab;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private TowerGenerationConfig _gunTowerGeneration;
        [SerializeField] private TowerGenerationConfig _rocketTowerGeneration;
        [SerializeField] private Tilemap _baseTileMap;
        [SerializeField] private Tilemap _environmentTileMap;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _enemySpawnPosition;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private TowerObject[] _gunTowers;
        [SerializeField] private TowerObject[] _rocketTowers;
        [Space(15)] 
        [SerializeField] private Transform[] _pathPoints;

        private EnemyFactory _enemyFactory;
        
        private void Awake()
        {
            InitTowers();
            InitEnemies();
        }

        private void InitTowers()
        {
            new InputHandler();
            Map baseMap = new Map(_baseTileMap);
            Map environmentMap = new Map(_environmentTileMap);
            
            var container = new DiContainer();
            var towerFactory = new TowerFactory(container, baseMap, environmentMap, _prefab, _towerParent, this);

            MergeHandler.Init(towerFactory);
            
            foreach (var tower in _gunTowers)
            {
                Tower model = new Tower()
                {
                    ID = tower.GetInstanceID(),
                    Level = 1,
                    Position = baseMap.GetCellPosition(tower.transform.position),
                    Type = _gunTowerGeneration.TowersType
                };
                
                TowerProxy proxy = new TowerProxy(model);
                ITowerAttacker attacker = new TowerRegularAttacker(tower.CollisionHandler, this);
                
                tower.Init(_gunTowerGeneration, proxy, baseMap, environmentMap, attacker);
            }
            
            foreach (var tower in _rocketTowers)
            {
                Tower model = new Tower()
                {
                    ID = tower.GetInstanceID(),
                    Level = 1,
                    Position = baseMap.GetCellPosition(tower.transform.position),
                    Type = _rocketTowerGeneration.TowersType
                };
                
                TowerProxy proxy = new TowerProxy(model);
                ITowerAttacker attacker = new TowerRegularAttacker(tower.CollisionHandler, this);
                
                tower.Init(_rocketTowerGeneration, proxy, baseMap, environmentMap, attacker);
            }
        }

        private void InitEnemies()
        {
            DiContainer container = new DiContainer();
            
            _enemyFactory = new EnemyFactory(container, _enemyPrefab, _enemyParent);
            List<Vector3> path = _pathPoints.Select(x => x.position).ToList();
            IWaveSpawnerService waveSpawnerService = new WaveSpawnerService(_enemyFactory, _gameConfig.Waves, path, _enemySpawnPosition.transform.position, this);

            waveSpawnerService.SpawnNextWave();
            waveSpawnerService.OnWaveCompleted += () => { waveSpawnerService.SpawnNextWave(); };
            waveSpawnerService.OnAllWavesCompleted += () => { Debug.Log("LEVEL COMPLETE!"); };
        }
    }
}