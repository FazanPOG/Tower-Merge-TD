using R3;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootView _uiGameplayRootPrefab;

        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams gameplayEnterParams)
        {
            gameplayContainer.UnbindAll();
            
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiGameplayRoot = Instantiate(_uiGameplayRootPrefab);
            uiRoot.AttackSceneUI(uiGameplayRoot.gameObject);
            
            var exitSceneSignalSubj = new Subject<Unit>();
            uiGameplayRoot.Bind(exitSceneSignalSubj);

            var gameplayBinder = new GameplayBinder(gameplayContainer);
            gameplayBinder.Bind();

            Debug.Log($"GAMEPLAY ENTER PARAMS: save file = {gameplayEnterParams.SaveFileName}, level number = {gameplayEnterParams.LevelNumber}");
            
            var mainMenuEnterParams = new MainMenuEnterParams("result");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);

            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            return exitToMainMenuSceneSignal;
        }
    }
}