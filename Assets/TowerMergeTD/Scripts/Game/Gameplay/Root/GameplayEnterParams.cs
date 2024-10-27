using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string SaveFileName { get; }
        public int LevelNumber { get; }
        
        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.Gameplay)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}