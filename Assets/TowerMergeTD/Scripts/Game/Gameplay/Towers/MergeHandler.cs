using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public static class MergeHandler
    {
        private static TowerFactory _towerFactory;

        public static void Init(TowerFactory towerFactory)
        {
            _towerFactory = towerFactory;
        }
        
        public static bool TryMerge(TowerGenerationConfig generation, TowerObject firstMergedTower, TowerObject secondMergedTower)
        {
            if(generation.IsLastInGeneration(firstMergedTower.Level) || generation.IsLastInGeneration(secondMergedTower.Level))
                return false;
            
            if(firstMergedTower.Type != secondMergedTower.Type)
                return false;

            if (firstMergedTower.Level != secondMergedTower.Level)
                return false;
            
            var spawnPosition = secondMergedTower.transform.position;
            firstMergedTower.DestroySelf();
            secondMergedTower.DestroySelf();
            
            _towerFactory.Create(generation.TowersType, spawnPosition, firstMergedTower.Level + 1);
            return true;
        }
    }
}