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
            _uiRoot.AttackSceneUI(uiMainMenuRoot.gameObject);
        
            var exitSceneSignalSubj = new Subject<Unit>();
            uiMainMenuRoot.Bind(exitSceneSignalSubj);
            
            Debug.Log($"MAIN MENU ENTER PARAMS: {mainMenuEnterParams?.Result}");
            
            var projectConfig = mainMenuContainer.Resolve<ProjectConfig>();
            var gameplayEnterParams = new GameplayEnterParams(projectConfig.LevelConfigs.First());
            _mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            
            var exitToGameplaySceneSignal = exitSceneSignalSubj.Select(_ => _mainMenuExitParams);
            return exitToGameplaySceneSignal;
        }
    }
}
