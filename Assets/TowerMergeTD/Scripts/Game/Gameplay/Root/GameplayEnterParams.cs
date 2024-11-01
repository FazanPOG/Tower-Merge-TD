using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public LevelConfig LevelConfig { get; }
        
        public GameplayEnterParams(LevelConfig levelConfig) : base(Scenes.Gameplay)
        {
            LevelConfig = levelConfig;
        }
    }
}