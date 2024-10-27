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
        [SerializeField] private TowerGenerationConfig _gunTowerGeneration;
        [SerializeField] private TowerGenerationConfig _rocketTowerGeneration;
        [SerializeField] private Tilemap _baseTileMap;
        [SerializeField] private Tilemap _environmentTileMap;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private TowerObject[] _gunTowers;
        [SerializeField] private TowerObject[] _rocketTowers;
        [Space(15)] [SerializeField] private Transform[] _pathPoints;

        private List<TowerFactory> _towerFactories;
        
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
            _towerFactories = new List<TowerFactory>()
            {
                new TowerFactory(container, baseMap, environmentMap, _prefab, _gunTowerGeneration, this),
                new TowerFactory(container, baseMap, environmentMap, _prefab, _rocketTowerGeneration, this),
            };

            MergeHandler.Init(_towerFactories.ToArray());
            
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
            List<Vector3> path = _pathPoints.Select(x => x.position).ToList();
            _enemy?.Init(path);
        }
    }
}