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
        private readonly TowerObject _prefab;
        private readonly TilemapCoordinator _tilemapCoordinator;
        private readonly Transform _parent;
        private readonly MonoBehaviour _monoBehaviourContext;

        public TowerFactory(
            DiContainer diContainer, 
            TowerObject prefab,
            TilemapCoordinator tilemapCoordinator,
            Transform parent,
            MonoBehaviour monoBehaviourContext)
        {
            _diContainer = diContainer;
            _prefab = prefab;
            _tilemapCoordinator = tilemapCoordinator;
            _parent = parent;
            _monoBehaviourContext = monoBehaviourContext;
        }

        public TowerObject Create(InputHandler inputHandler, TowerGenerationConfig generation, Vector2 spawnPosition, int towerLevel)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(_prefab, _parent);
            instance.transform.position = spawnPosition;

            TowerProxy proxy = new TowerProxy(CreateTowerModel(generation, instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(generation, instance.CollisionHandler);
            instance.Init(inputHandler, generation, proxy, _tilemapCoordinator, attacker);

            instance.name = $"{instance.Type} {towerLevel}";
            return instance;
        }

        private Tower CreateTowerModel(TowerGenerationConfig generation, TowerObject towerObject, int towerLevel, Vector2 spawnPosition)
        {
            Tower towerModel = new Tower()
            {
                ID = towerObject.GetInstanceID(),
                Level = towerLevel,
                Position = _tilemapCoordinator.GetCellPosition(TilemapType.Base, spawnPosition),
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