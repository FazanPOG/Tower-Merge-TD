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

        public Observable<MainMenuExitParams> Run(DiContainer mainMenuContainer, MainMenuEnterParams mainMenuEnterParams)
        {
            mainMenuContainer.UnbindAll();
            
            var _uiRoot = mainMenuContainer.Resolve<UIRootView>();
            var uiMainMenuRoot = Instantiate(_uiMainMenuRootPrefab);
            _uiRoot.AttackSceneUI(uiMainMenuRoot.gameObject);
        
            var exitSceneSignalSubj = new Subject<Unit>();
            uiMainMenuRoot.Bind(exitSceneSignalSubj);
            
            Debug.Log($"MAIN MENU ENTER PARAMS: {mainMenuEnterParams?.Result}");

            var saveFileName = "save file main test";
            var levelNumber = Random.Range(1, 200);
            var gameplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);

            var exitToGameplaySceneSignal = exitSceneSignalSubj.Select(_ => mainMenuExitParams);
            return exitToGameplaySceneSignal;
        }
    }
}
