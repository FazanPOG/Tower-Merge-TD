using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public Level Level { get; }
        
        public GameplayEnterParams(Level level) : base(Scenes.Gameplay)
        {
            Level = level;
        }
    }
}