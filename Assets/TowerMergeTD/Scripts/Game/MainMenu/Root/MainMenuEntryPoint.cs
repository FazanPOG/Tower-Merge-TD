using R3;
using TowerMergeTD.Game.UI;
using TowerMergeTD.Game.UI.Root;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.Utils.Debug;
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
            
            var uiRoot = mainMenuContainer.Resolve<UIRootView>();
            var uiMainMenuRoot = Instantiate(_uiMainMenuRootPrefab);
            uiRoot.AttachSceneUI(uiMainMenuRoot.gameObject);
        
            var exitSceneSignal = new ReactiveProperty<int>();
            uiMainMenuRoot.Bind(exitSceneSignal, mainMenuContainer);
            
            if(TryGetComponent(out MainMenuDebug mainMenuDebug))
                mainMenuDebug.Init(mainMenuContainer);

            exitSceneSignal.Skip(1).Subscribe(levelIndex =>
            {
                var gameplayEnterParams = new GameplayEnterParams(levelIndex);
                _mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            });

            var exitToGameplaySceneSignal = exitSceneSignal.Select(_ => _mainMenuExitParams);
            return exitToGameplaySceneSignal;
        }
    }
}
