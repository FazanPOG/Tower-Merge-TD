using System.Collections;
using R3;
using TowerMergeTD.API;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI.Root;
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

        private readonly DiContainer _rootContainer = new DiContainer();
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly UIRootView _uiRootView;
        private readonly BackgroundMusic _backgroundMusic;
        private readonly AudioPlayer _audioPlayer;
        private readonly ProjectConfig _projectConfig;
        
        private DiContainer _cashedSceneContainer;
        private bool _isDataLoaded = false;
        
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
            
            var audioClipsConfig = Resources.Load<AudioClipsConfig>("AudioClipsConfig");
            
            APIBinder apiBinder = new APIBinder(_rootContainer);
            apiBinder.Bind();
            
            var backgroundMusic = Resources.Load<BackgroundMusic>("BackgroundMusic");
            _backgroundMusic = Object.Instantiate(backgroundMusic);
            Object.DontDestroyOnLoad(_backgroundMusic.gameObject);
            
            var audioPlayer = Resources.Load<AudioPlayer>("AudioPlayer");
            _audioPlayer = Object.Instantiate(audioPlayer);
            Object.DontDestroyOnLoad(_audioPlayer.gameObject);
            
            var gameStateProvider = new PlayerPrefsGameStateProvider(_projectConfig, _monoBehaviourWrapper);
            var timerService = new TimerService(_monoBehaviourWrapper);
            
            _rootContainer.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().FromInstance(gameStateProvider).AsSingle().NonLazy();
            _rootContainer.Bind<ICurrencyProvider>().To<PlayerPrefsCurrencyProvider>().FromNew().AsSingle().NonLazy();
            _rootContainer.Bind<UIRootView>().FromInstance(_uiRootView).AsSingle().NonLazy();
            _rootContainer.Bind<MonoBehaviourWrapper>().FromInstance(_monoBehaviourWrapper).AsSingle().NonLazy();
            _rootContainer.Bind<ProjectConfig>().FromInstance(_projectConfig).AsSingle().NonLazy();
            _rootContainer.Bind<PrefabReferencesConfig>().FromInstance(prefabReferencesConfig).AsSingle().NonLazy();
            _rootContainer.Bind<TimerService>().FromInstance(timerService).AsSingle().NonLazy();
            _rootContainer.Bind<AudioClipsConfig>().FromInstance(audioClipsConfig).AsSingle().NonLazy();
            _rootContainer.Bind<BackgroundMusic>().FromInstance(_backgroundMusic).AsSingle().NonLazy();
            _rootContainer.Bind<AudioPlayer>().FromInstance(_audioPlayer).AsSingle().NonLazy();
            
        }
        
        private void StartGame()
        {
            _monoBehaviourWrapper.StartCoroutine(LoadPlayerData());
            
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.Gameplay)
            {
                var enterParams = new GameplayEnterParams(0);
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

        private IEnumerator LoadPlayerData()
        {
            yield return waitAPILoad();
            yield return loadPlayerCurrency();
            yield return loadGameState();
            yield return loadLocalization();
            
            _isDataLoaded = true;

            IEnumerator waitAPILoad()
            {
                var apiEnvironment = _rootContainer.Resolve<IAPIEnvironmentService>();
                yield return new WaitUntil(() => apiEnvironment.IsReady);
            }
            IEnumerator loadPlayerCurrency()
            {
                var currencyProvider = _rootContainer.Resolve<ICurrencyProvider>();
            
                bool isCurrencyLoaded = false;
                currencyProvider.LoadCurrency().Subscribe(_ => isCurrencyLoaded = true);
                yield return new WaitUntil(() => isCurrencyLoaded);

                _rootContainer.Bind<PlayerCoinsProxy>().FromInstance(currencyProvider.Coins).AsSingle().NonLazy();
                _rootContainer.Bind<PlayerGemsProxy>().FromInstance(currencyProvider.Gems).AsSingle().NonLazy();
            }
            IEnumerator loadGameState()
            {
                bool isGameStateLoaded = false;
                _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
                yield return new WaitUntil(() => isGameStateLoaded);
            }
            IEnumerator loadLocalization()
            {
                ILocalizationProvider provider = new YandexGamesLocalizationProvider(_projectConfig);
                
                bool isLocalizationLoaded = false;
                provider.LoadLocalizationAsset().Subscribe(asset =>
                {
                    _rootContainer.Bind<ILocalizationAsset>().FromInstance(asset).AsSingle().NonLazy();
                    isLocalizationLoaded = true;
                });
                yield return new WaitUntil(() => isLocalizationLoaded);
            }
        }
        
        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null)
        {
            yield return new WaitUntil(() => _isDataLoaded);
            
            _cashedSceneContainer?.UnbindAll();
            
            _uiRootView.ShowLoadingScreen();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.MainMenu);
            
            //
            bool isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
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
            yield return new WaitUntil(() => _isDataLoaded);
            
            _cashedSceneContainer?.UnbindAll();
            
            _uiRootView.ShowLoadingScreen();
            
            yield return LoadScene(Scenes.Boot);
            yield return LoadScene(Scenes.Gameplay);
            
            //
            var gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            var gameplayContainer = _cashedSceneContainer = new DiContainer(_rootContainer);

            gameplayEntryPoint.Run(gameplayContainer, gameplayEnterParams).Skip(1).Subscribe(gameplayExitParams =>
            {
                var targetSceneName = gameplayExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.Gameplay)
                {
                    _monoBehaviourWrapper.StartCoroutine(LoadAndStartGameplay(gameplayExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
                else if (targetSceneName == Scenes.MainMenu)
                {
                    _monoBehaviourWrapper.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.TargetSceneEnterParams.As<MainMenuEnterParams>())); 
                }
            });
            
            _uiRootView.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
