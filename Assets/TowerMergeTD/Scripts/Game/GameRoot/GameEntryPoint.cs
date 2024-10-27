using System.Collections;
using R3;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.MainMenu.Root;
using TowerMergeTD.Scripts.Game.State;
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

            var gameStateProvider = new PlayerPrefsGameStateProvider();

            _rootContainer.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().FromInstance(gameStateProvider).AsSingle().NonLazy();
            _rootContainer.Bind<UIRootView>().FromInstance(_uiRootView).AsSingle().NonLazy();
            _rootContainer.Bind<MonoBehaviourWrapper>().FromInstance(_monoBehaviourWrapper).AsSingle().NonLazy();
        }
        
        private void StartGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.Gameplay)
            {
                var enterParams = new GameplayEnterParams("test.save", 1); 
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
            _uiRootView.ShowLoadingScreen();
            _cashedSceneContainer?.UnbindAll();
            
            _monoBehaviourWrapper.ClearTickableList();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.MainMenu);
            yield return new WaitForSeconds(1f);
            
            //
            bool isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);
            
            var mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            var mainMenuContainer = _cashedSceneContainer = new DiContainer(_rootContainer);
            
            mainMenuEntryPoint.Run(mainMenuContainer, mainMenuEnterParams).Subscribe(mainMenuExitParams =>
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
            _uiRootView.ShowLoadingScreen();
            _cashedSceneContainer?.UnbindAll();
            _monoBehaviourWrapper.ClearTickableList();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.Gameplay);
            yield return new WaitForSeconds(1f);
            
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
