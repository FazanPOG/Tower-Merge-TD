using System.Linq;
using R3;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.GameRoot;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRootView _uiMainMenuRootPrefab;

        private MainMenuExitParams _mainMenuExitParams;
        
        public Observable<MainMenuExitParams> Run(DiContainer mainMenuContainer, MainMenuEnterParams mainMenuEnterParams)
        {
            mainMenuContainer.UnbindAll();
            
            var _uiRoot = mainMenuContainer.Resolve<UIRootView>();
            var uiMainMenuRoot = Instantiate(_uiMainMenuRootPrefab);
            _uiRoot.AttachSceneUI(uiMainMenuRoot.gameObject);
        
            var exitSceneSignal = new ReactiveProperty<int>();
            uiMainMenuRoot.Bind(exitSceneSignal);
            
            Debug.Log($"MAIN MENU ENTER PARAMS: {mainMenuEnterParams?.Result}");

            exitSceneSignal.Skip(1).Subscribe(levelNumber =>
            {
                var projectConfig = mainMenuContainer.Resolve<ProjectConfig>();
                var level = projectConfig.Levels[levelNumber - 1];

                var gameplayEnterParams = new GameplayEnterParams(level);
                _mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            });

            var exitToGameplaySceneSignal = exitSceneSignal.Select(_ => _mainMenuExitParams);
            return exitToGameplaySceneSignal;
        }
    }
}
