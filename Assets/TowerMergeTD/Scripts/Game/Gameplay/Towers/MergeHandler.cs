using System.Linq;
using ModestTree;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public static class MergeHandler
    {
        private static TowerFactory[] _towerFactories;

        public static void Init(TowerFactory[] towerFactories)
        {
            _towerFactories = towerFactories;
        }
        
        public static bool TryMerge(TowerGenerationConfig generation, TowerObject firstMergedTower, TowerObject secondMergedTower)
        {
            if (_towerFactories.IsEmpty())
                return false;
            
            if(generation.IsLastInGeneration(firstMergedTower.Level) || generation.IsLastInGeneration(secondMergedTower.Level))
                return false;
            
            if(firstMergedTower.Type != secondMergedTower.Type)
                return false;

            if (firstMergedTower.Level != secondMergedTower.Level)
                return false;
            
            var spawnPosition = secondMergedTower.transform.position;
            firstMergedTower.DestroySelf();
            secondMergedTower.DestroySelf();
            
            var factory = FindFirstCorrectFactory(generation);
            //TODO: change parent
            factory.Create(Camera.main.transform, spawnPosition, firstMergedTower.Level + 1);
            return true;
        }

        private static TowerFactory FindFirstCorrectFactory(TowerGenerationConfig generation)
        {
            return _towerFactories.First(x => x.Generation.TowersType == generation.TowersType);
        }
    }
}