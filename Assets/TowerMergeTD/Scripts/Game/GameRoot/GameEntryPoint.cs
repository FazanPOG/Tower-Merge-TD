using System.Collections;
using System.Linq;
using R3;
using TowerMergeTD.Game.State;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.MainMenu.Root;
using TowerMergeTD.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TowerMergeTD.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private UIRootView _uiRootView;
        private ProjectConfig _projectConfig;
        private readonly DiContainer _rootContainer = new DiContainer();
        private DiContainer _cashedSceneContainer;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 144;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            _instance = new GameEntryPoint();
            
            _instance.StartGame();
        }

        private GameEntryPoint()
        {
            _monoBehaviourWrapper = new GameObject("[MonoBehaviourWrapper]").AddComponent<MonoBehaviourWrapper>();
            Object.DontDestroyOnLoad(_monoBehaviourWrapper.gameObject);

            var prefabUIRootView = Resources.Load<UIRootView>("UIRoot");
            _uiRootView = Object.Instantiate(prefabUIRootView);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);

            _projectConfig = Resources.Load<ProjectConfig>("ProjectConfig");
            var prefabReferencesConfig = Resources.Load<PrefabReferencesConfig>("PrefabReferencesConfig");
            var gameStateProvider = new PlayerPrefsGameStateProvider();

            _rootContainer.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().FromInstance(gameStateProvider).AsSingle().NonLazy();
            _rootContainer.Bind<UIRootView>().FromInstance(_uiRootView).AsSingle().NonLazy();
            _rootContainer.Bind<MonoBehaviourWrapper>().FromInstance(_monoBehaviourWrapper).AsSingle().NonLazy();
            _rootContainer.Bind<ProjectConfig>().FromInstance(_projectConfig).AsSingle().NonLazy();
            _rootContainer.Bind<PrefabReferencesConfig>().FromInstance(prefabReferencesConfig).AsSingle().NonLazy();
        }
        
        private void StartGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.Gameplay)
            {
                var enterParams = new GameplayEnterParams(_projectConfig.Levels.First());
                _monoBehaviourWrapper.StartCoroutine(LoadAndStartGameplay(enterParams));
                return;
            }
            
            if (sceneName == Scenes.MainMenu)
            {
                _monoBehaviourWrapper.StartCoroutine(LoadAndStartMainMenu());
                return;
            }
            
            if (sceneName != Scenes.Boot)
            {
                return;
            }
#endif

            _monoBehaviourWrapper.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null)
        {
            _cashedSceneContainer?.UnbindAll();
            
            _uiRootView.ShowLoadingScreen();

            _monoBehaviourWrapper.ClearTickableList();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.MainMenu);
            
            //
            bool isGameStateLoaded = false;
            var gameStateProvider = _rootContainer.Resolve<IGameStateProvider>();
            gameStateProvider.Init(_projectConfig);
            gameStateProvider.LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);
            
            var mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            var mainMenuContainer = _cashedSceneContainer = new DiContainer(_rootContainer);
            
            mainMenuEntryPoint.Run(mainMenuContainer, mainMenuEnterParams).Skip(1).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.Gameplay)
                {
                    _monoBehaviourWrapper.StartCoroutine(LoadAndStartGameplay( mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });
            
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadAndStartGameplay(GameplayEnterParams gameplayEnterParams)
        {
            _cashedSceneContainer?.UnbindAll();
            
            _uiRootView.ShowLoadingScreen();
            _monoBehaviourWrapper.ClearTickableList();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.Gameplay);
            
            //
            var gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            var gameplayContainer = _cashedSceneContainer = new DiContainer(_rootContainer);
            
            gameplayEntryPoint.Run(gameplayContainer, gameplayEnterParams).Subscribe(gameplayExitParams =>
            {
                _monoBehaviourWrapper.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams)); 
            });

            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
