using Game.State;
using TowerMergeTD.Game.State;
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
        private readonly Transform _parent;
        private readonly MonoBehaviour _monoBehaviourContext;

        public TowerFactory(
            DiContainer diContainer, 
            Map baseMap, 
            Map environmentMap, 
            TowerObject prefab, 
            Transform parent,
            MonoBehaviour monoBehaviourContext)
        {
            _diContainer = diContainer;
            _baseMap = baseMap;
            _environmentMap = environmentMap;
            _prefab = prefab;
            _parent = parent;
            _monoBehaviourContext = monoBehaviourContext;
        }

        public TowerObject Create(TowerGenerationConfig generation, Vector2 spawnPosition, int towerLevel)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(_prefab, _parent);
            instance.transform.position = spawnPosition;

            TowerProxy proxy = new TowerProxy(CreateTowerModel(generation, instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(generation, instance.CollisionHandler);
            instance.Init(generation, proxy, _baseMap, _environmentMap, attacker);

            instance.name = $"{instance.Type} {towerLevel}";
            return instance;
        }

        private Tower CreateTowerModel(TowerGenerationConfig generation, TowerObject towerObject, int towerLevel, Vector2 spawnPosition)
        {
            Tower towerModel = new Tower()
            {
                ID = towerObject.GetInstanceID(),
                Level = towerLevel,
                Position = _baseMap.GetCellPosition(spawnPosition),
                Type = generation.TowersType
            };

            return towerModel;
        }
        
        private ITowerAttacker GetTowerAttacker(TowerGenerationConfig generation, TowerCollisionHandler collisionHandler)
        {
            if (generation.TowersType == GUN_TOWER_TYPE)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            if (generation.TowersType == ROCKET_TOWER_TYPE)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            throw new MissingReferenceException($"Missing tower type: {generation.TowersType}");
        }
    }
}