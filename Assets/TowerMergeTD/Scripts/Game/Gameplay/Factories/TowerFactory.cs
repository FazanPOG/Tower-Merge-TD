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
        private readonly TowerObject _prefab;
        private readonly MapCoordinator _mapCoordinator;
        private readonly InputHandler _inputHandler;
        private readonly Transform _parent;
        private readonly MonoBehaviour _monoBehaviourContext;
        private readonly IPauseService _pauseService;

        public TowerFactory(
            DiContainer diContainer,
            TowerGenerationConfig[] generations,
            PrefabReferencesConfig prefabReferences,
            MapCoordinator mapCoordinator,
            InputHandler inputHandler,
            Transform parent,
            MonoBehaviourWrapper monoBehaviourContext,
            IPauseService pauseService)
        {
            _diContainer = diContainer;
            _generations = generations;
            _prefab = prefabReferences.TowerPrefab;
            _mapCoordinator = mapCoordinator;
            _inputHandler = inputHandler;
            _parent = parent;
            _monoBehaviourContext = monoBehaviourContext;
            _pauseService = pauseService;
        }

        public TowerObject Create(TowerType towerType, Vector2 spawnPosition, int towerLevel)
        {
            var generation = GetTowerGenerationConfig(towerType);
            
            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(_prefab, _parent);
            instance.transform.position = spawnPosition;

            TowerProxy proxy = new TowerProxy(CreateTowerModel(generation, instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(generation, instance.CollisionHandler);
            instance.Init(_inputHandler, generation, proxy, _mapCoordinator, attacker, _pauseService);

            instance.name = $"{instance.Type} {towerLevel}";
            return instance;
        }

        public int GetCreateCost(TowerType towerType)
        {
            var generation = GetTowerGenerationConfig(towerType);
            return generation.CreateCost;
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
        
        private ITowerAttacker GetTowerAttacker(TowerGenerationConfig generation, TowerCollisionHandler collisionHandler)
        {
            if (generation.TowersType == TowerType.Gun)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            if (generation.TowersType == TowerType.Rocket)
            {
                return new TowerRegularAttacker(collisionHandler, _monoBehaviourContext);
            }
            
            throw new MissingReferenceException($"Missing tower type: {generation.TowersType}");
        }
    }
}