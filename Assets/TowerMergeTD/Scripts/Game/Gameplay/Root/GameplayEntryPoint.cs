using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.UI.Root;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;
using TowerMergeTD.Utils.Debug;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootView _uiGameplayRootPrefab;

        private ReactiveProperty<SceneEnterParams> _exitSceneSignalSubj;
        private ProjectConfig _projectConfig;
        private GameStateMachine _gameStateMachine;
        
        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams gameplayEnterParams)
        {
            BindDependencies(gameplayContainer, gameplayEnterParams);
            StartGameplay();
            
            //TODO: Change MainMenuEnterParams to end level stats (score + ...)

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

            _projectConfig = gameplayContainer.Resolve<ProjectConfig>();
            
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiGameplayRoot = Instantiate(_uiGameplayRootPrefab);
            uiRoot.AttachSceneUI(uiGameplayRoot.gameObject);

            var level = Instantiate(_projectConfig.Levels[gameplayEnterParams.LevelIndex]);
            level.name = $"{level.name}";

            _exitSceneSignalSubj = new ReactiveProperty<SceneEnterParams>();
            
            var gameplayBinder = new GameplayBinder(gameplayContainer);
            _gameStateMachine = gameplayBinder.Bind(level);
            
            uiGameplayRoot.Bind(_exitSceneSignalSubj, gameplayContainer, gameplayEnterParams.LevelIndex);
            
            if(TryGetComponent(out GameplayDebug gameplayDebug))
                gameplayDebug.Init(gameplayContainer);
        }

        private void StartGameplay()
        {
            _gameStateMachine.EnterIn<BootState>();
        }
    }
}