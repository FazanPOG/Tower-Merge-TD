using System;
using Game.State;
using TowerMergeTD.Game.State;
using TowerMergeTD.Utils;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerFactory
    {
        private const string GUN_TOWER_TYPE = "GunTower";
        private const string ROCKET_TOWER_TYPE = "RocketTower";
        
        private readonly DiContainer _diContainer;
        private readonly Map _baseMap;
        private readonly Map _environmentMap;
        private readonly TowerObject _prefab;
        private readonly MonoBehaviour _monoBehaviourContext;

        public TowerGenerationConfig Generation { get; }

        public TowerFactory(
            DiContainer diContainer, 
            Map baseMap, 
            Map environmentMap, 
            TowerObject prefab, 
            TowerGenerationConfig generation, 
            MonoBehaviour monoBehaviourContext)
        {
            _diContainer = diContainer;
            _baseMap = baseMap;
            _environmentMap = environmentMap;
            _prefab = prefab;
            _monoBehaviourContext = monoBehaviourContext;
            Generation = generation;
        }

        public TowerObject Create(Transform parent, Vector2 spawnPosition, int towerLevel)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(_prefab, parent);
            instance.transform.position = spawnPosition;

            TowerProxy proxy = new TowerProxy(CreateTowerModel(instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(instance.CollisionHandler);
            instance.Init(Generation, proxy, _baseMap, _environmentMap, attacker);

            instance.name = $"{instance.Type} {towerLevel}";
            return instance;
        }

        private Tower CreateTowerModel(TowerObject towerObject, int towerLevel, Vector2 spawnPosition)
        {
            Tower towerModel = new Tower()
            {
                ID = towerObject.GetInstanceID(),
                Level = towerLevel,
                Position = _baseMap.GetCellPosition(spawnPosition),
                Type = Generation.TowersType
            };

            return towerModel;
        }
        
        private ITowerAttacker GetTowerAttacker(TowerCollisionHandler collisionHandler)
        {
            if (Generation.TowersType == GUN_TOWER_TYPE)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            if (Generation.TowersType == ROCKET_TOWER_TYPE)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            throw new MissingReferenceException($"Missing tower type: {Generation.TowersType}");
        }
    }
}