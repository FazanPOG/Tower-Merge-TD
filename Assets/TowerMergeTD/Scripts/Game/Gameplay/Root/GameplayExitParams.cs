using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public GameplayExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}