using TowerMergeTD.GameRoot;

namespace TowerMergeTD.MainMenu.Root
{
    public class MainMenuEnterParams : SceneEnterParams
    {
        public string Result { get; }

        public MainMenuEnterParams(string result) : base(Scenes.MainMenu)
        {
            Result = result;
        }
    }
}