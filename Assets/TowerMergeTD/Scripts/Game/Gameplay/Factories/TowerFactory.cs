using System.Linq;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TowerGenerationConfig[] _generations;
        private readonly PrefabReferencesConfig _prefabReferences;
        private readonly MapCoordinator _mapCoordinator;
        private readonly IInput _input;
        private readonly Transform _parent;
        private readonly MonoBehaviour _monoBehaviourContext;
        private readonly IPauseService _pauseService;

        public TowerFactory(
            DiContainer diContainer,
            TowerGenerationConfig[] generations,
            PrefabReferencesConfig prefabReferences,
            MapCoordinator mapCoordinator,
            IInput input,
            Transform parent,
            MonoBehaviourWrapper monoBehaviourContext,
            IPauseService pauseService)
        {
            _diContainer = diContainer;
            _generations = generations;
            _prefabReferences = prefabReferences;
            _mapCoordinator = mapCoordinator;
            _input = input;
            _parent = parent;
            _monoBehaviourContext = monoBehaviourContext;
            _pauseService = pauseService;
        }

        public TowerObject Create(TowerType towerType, Vector2 spawnPosition, int towerLevel)
        {
            var generation = GetTowerGenerationConfig(towerType);
            var prefab = GetTowerPrefab(towerType);
            
            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(prefab, _parent);
            instance.transform.position = spawnPosition;
            
            TowerProxy proxy = new TowerProxy(CreateTowerModel(generation, instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(generation, instance.CollisionHandler, towerLevel, instance.View);
            
            instance.Init(_input, generation, proxy, _mapCoordinator, attacker, _pauseService);
            instance.name = $"{instance.Type} {towerLevel}";
            
            return instance;
        }

        public int GetCreateCost(TowerType towerType)
        {
            var generation = GetTowerGenerationConfig(towerType);
            return generation.CreateCost;
        }

        private TowerObject GetTowerPrefab(TowerType towerType)
        {
            switch (towerType)
            {
                case TowerType.Gun:
                    return _prefabReferences.GunTowerPrefab; 
                
                case TowerType.Rocket:
                    return _prefabReferences.RocketTowerPrefab;
                
                default:
                    throw new MissingReferenceException($"Missing tower type: {towerType}");
            }
        }
        
        private TowerGenerationConfig GetTowerGenerationConfig(TowerType towerType)
        {
            switch (towerType)
            {
                case TowerType.Gun:
                    return _generations.First(x => x.TowersType == TowerType.Gun); 
                
                case TowerType.Rocket:
                    return _generations.First(x => x.TowersType == TowerType.Rocket);
                
                case TowerType.Laser:
                    return _generations.First(x => x.TowersType == TowerType.Laser);

                default:
                    throw new MissingReferenceException($"Missing tower type: {towerType}");
            }
        }
        
        private Tower CreateTowerModel(TowerGenerationConfig generation, TowerObject towerObject, int towerLevel, Vector2 spawnPosition)
        {
            Tower towerModel = new Tower()
            {
                ID = towerObject.GetInstanceID(),
                Level = towerLevel,
                Position = _mapCoordinator.GetCellPosition(TilemapType.Base, spawnPosition),
                Type = generation.TowersType
            };

            return towerModel;
        }
        
        private ITowerAttacker GetTowerAttacker
            (
            TowerGenerationConfig generation, 
            TowerCollisionHandler collisionHandler, 
            int towerLevel, 
            TowerObjectView towerObjectView
            )
        {
            if (generation.TowersType == TowerType.Gun)
            {
                return new TowerRegularAttacker(collisionHandler);
            }
            
            if (generation.TowersType == TowerType.Rocket)
            {
                if(towerLevel < 3)
                    return new TowerRocketAttacker(collisionHandler, _prefabReferences.SmallRocket, towerObjectView as RocketTowerObjectView);
                else
                    return new TowerRocketAttacker(collisionHandler, _prefabReferences.BigRocket, towerObjectView as RocketTowerObjectView);
            }
            
            throw new MissingReferenceException($"Missing tower type: {generation.TowersType}");
        }
    }
}