using R3;
using TowerMergeTD.Game.Gameplay;
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
            uiRoot.AttachSceneUI(uiGameplayRoot.gameObject);

            var level = gameplayContainer.InstantiatePrefabForComponent<Level>(gameplayEnterParams.Level);
            level.name = $"{gameplayEnterParams.Level.name}";
            
            var exitSceneSignalSubj = new Subject<Unit>();

            var gameplayBinder = new GameplayBinder(gameplayContainer);
            gameplayBinder.Bind(level);
            
            uiGameplayRoot.Bind(exitSceneSignalSubj, gameplayContainer);

            Debug.Log($"GAMEPLAY ENTER PARAMS: {level.name}");
            StartGameplay(gameplayContainer);
            
            //TODO: Change MainMenuEnterParams to end level stats (score + ...)
            var mainMenuEnterParams = new MainMenuEnterParams("result");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);

            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            return exitToMainMenuSceneSignal;
        }

        private void StartGameplay(DiContainer container)
        {
            GameStateMachine gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.EnterIn<BootState>();
        }
    }
}