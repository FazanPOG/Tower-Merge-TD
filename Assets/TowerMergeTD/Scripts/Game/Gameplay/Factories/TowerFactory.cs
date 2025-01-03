﻿using System.Linq;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
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
        private readonly IPauseService _pauseService;
        private readonly AudioPlayer _audioPlayer;
        private readonly IGameSpeedService _gameSpeedService;

        public TowerFactory(
            DiContainer diContainer,
            TowerGenerationConfig[] generations,
            PrefabReferencesConfig prefabReferences,
            MapCoordinator mapCoordinator,
            IInput input,
            Transform parent,
            IPauseService pauseService,
            AudioPlayer audioPlayer
            )
        {
            _diContainer = diContainer;
            _generations = generations;
            _prefabReferences = prefabReferences;
            _mapCoordinator = mapCoordinator;
            _input = input;
            _parent = parent;
            _pauseService = pauseService;
            _audioPlayer = audioPlayer;
        }

        public TowerObject Create(TowerType towerType, Vector2 spawnPosition, int towerLevel)
        {
            var prefab = GetTowerPrefab(towerType);

            var instance = _diContainer.InstantiatePrefabForComponent<TowerObject>(prefab, _parent);
            instance.transform.position = spawnPosition;
            
            TowerProxy proxy = new TowerProxy(CreateTowerModel(towerType, instance, towerLevel, spawnPosition));

            ITowerAttacker attacker = GetTowerAttacker(towerType, instance.CollisionHandler, towerLevel, instance.View);
            
            instance.Init(_input, proxy, _mapCoordinator, attacker, _pauseService, _audioPlayer);
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
            var prefab = _prefabReferences.TowersPrefab.FirstOrDefault(x => x.Type == towerType);

            if(prefab == null)
                throw new MissingReferenceException($"Missing tower type: {towerType}");
            
            return prefab;
        }
        
        private TowerGenerationConfig GetTowerGenerationConfig(TowerType towerType)
        {
            var towerGeneration = _generations.FirstOrDefault(x => x.TowersType == towerType);
            
            if(towerGeneration == null)
                throw new MissingReferenceException($"Missing tower type in ProjectConfig: {towerType}");

            return towerGeneration;
        }
        
        private Tower CreateTowerModel(TowerType towerType, TowerObject towerObject, int towerLevel, Vector2 spawnPosition)
        {
            Tower towerModel = new Tower()
            {
                ID = towerObject.GetInstanceID(),
                Level = towerLevel,
                Position = _mapCoordinator.GetCellPosition(TilemapType.Base, spawnPosition),
                Type = towerType
            };

            return towerModel;
        }
        
        private ITowerAttacker GetTowerAttacker
            (
            TowerType towerType, 
            TowerCollisionHandler collisionHandler, 
            int towerLevel, 
            TowerObjectView towerObjectView
            )
        {
            switch (towerType)
            {
                case TowerType.Gun:
                    return new TowerRegularAttacker(collisionHandler);
                
                case TowerType.Rocket:
                    if(towerLevel < 3)
                        return new TowerRocketAttacker(collisionHandler, _prefabReferences.SmallRocket, towerObjectView.As<RocketTowerObjectView>(), _audioPlayer);
                    else
                        return new TowerRocketAttacker(collisionHandler, _prefabReferences.BigRocket, towerObjectView.As<RocketTowerObjectView>(), _audioPlayer);
                
                case TowerType.Laser:
                    return new TowerLaserAttacker(collisionHandler);
                
                case TowerType.Sniper:
                    return new TowerSniperAttacker(collisionHandler);
                
                case TowerType.Electric:
                    return new TowerElectricAttacker(collisionHandler, _prefabReferences.Lightning, towerObjectView.As<ElectricTowerObjectView>(), _audioPlayer);
                    
                case TowerType.Impact:
                    return new TowerImpactAttacker(collisionHandler);
                
                case TowerType.Rapid:
                    return new TowerRegularAttacker(collisionHandler);
                
                case TowerType.GOD:
                    return new TowerRegularAttacker(collisionHandler);
                
                default:
                    throw new MissingReferenceException($"Missing tower type: {towerType}");
            }
        }
    }
}