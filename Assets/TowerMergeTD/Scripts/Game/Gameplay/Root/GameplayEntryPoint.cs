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

        private ReactiveProperty<SceneEnterParams> _exitSceneSignalSubj;
        
        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams gameplayEnterParams)
        {
            BindDependencies(gameplayContainer, gameplayEnterParams);
            StartGameplay(gameplayContainer);
            
            //TODO: Change MainMenuEnterParams to end level stats (score + ...)
            /*
            var currentLevelIndex = projectConfig.Levels.IndexOf(level);
            Level nextLevel = null;
            if (currentLevelIndex + 1 < projectConfig.Levels.Length)
                nextLevel = projectConfig.Levels[currentLevelIndex + 1];
            */

            GameplayExitParams exitParams = null;
            _exitSceneSignalSubj.Skip(1).Subscribe((sceneEnterParams) =>
            {
                if (sceneEnterParams.SceneName == Scenes.Gameplay)
                {
                    var enterParams = sceneEnterParams.As<GameplayEnterParams>();
                    exitParams = new GameplayExitParams(enterParams);
                }
                else if(sceneEnterParams.SceneName == Scenes.MainMenu)
                {
                    var enterParams = sceneEnterParams.As<MainMenuEnterParams>();
                    exitParams = new GameplayExitParams(enterParams);
                }
            });

            var exitToMainMenuSceneSignal = _exitSceneSignalSubj.Select(_ => exitParams);
            return exitToMainMenuSceneSignal;
        }

        private void BindDependencies(DiContainer gameplayContainer, GameplayEnterParams gameplayEnterParams)
        {
            gameplayContainer.UnbindAll();

            var projectConfig = gameplayContainer.Resolve<ProjectConfig>();

            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiGameplayRoot = Instantiate(_uiGameplayRootPrefab);
            uiRoot.AttachSceneUI(uiGameplayRoot.gameObject);

            var level = Instantiate(projectConfig.Levels[gameplayEnterParams.LevelIndex]);
            level.name = $"{level.name}";

            _exitSceneSignalSubj = new ReactiveProperty<SceneEnterParams>();
            
            var gameplayBinder = new GameplayBinder(gameplayContainer);
            gameplayBinder.Bind(level);
            
            uiGameplayRoot.Bind(_exitSceneSignalSubj, gameplayContainer, gameplayEnterParams.LevelIndex);
            
            Debug.Log($"GAMEPLAY ENTER PARAMS: {level.name}");
        }
        
        private void StartGameplay(DiContainer container)
        {
            var gameStateMachine = container.Resolve<GameStateMachine>();
            gameStateMachine.EnterIn<BootState>();
        }
    }
}