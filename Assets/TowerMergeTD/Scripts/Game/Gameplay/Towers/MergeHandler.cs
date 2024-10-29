using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public static class MergeHandler
    {
        private static TowerFactory _towerFactory;
        private static InputHandler _inputHandler;

        public static void Init(TowerFactory towerFactory, InputHandler inputHandler)
        {
            if(_towerFactory != null || _inputHandler != null)
                return;
            
            _towerFactory = towerFactory;
            _inputHandler = inputHandler;
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
            
            _towerFactory.Create(_inputHandler, generation, spawnPosition, firstMergedTower.Level + 1);
            return true;
        }
    }
}