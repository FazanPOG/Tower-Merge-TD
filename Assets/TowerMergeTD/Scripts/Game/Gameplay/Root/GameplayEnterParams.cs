using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public int LevelIndex { get; }
        
        public GameplayEnterParams(int levelIndex) : base(Scenes.Gameplay)
        {
            LevelIndex = levelIndex;
        }
    }
}