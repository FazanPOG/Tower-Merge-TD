using GamePush;
using R3;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
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
        [SerializeField] private BackgroundMusic _backgroundMusicPrefab;

        private DiContainer _mainMenuContainer;
        private MainMenuExitParams _mainMenuExitParams;
        private UIMainMenuRootView _uiMainMenuRoot;
        
        public Observable<MainMenuExitParams> Run(DiContainer mainMenuContainer, MainMenuEnterParams mainMenuEnterParams)
        {
            _mainMenuContainer = mainMenuContainer;
            
            _mainMenuContainer.UnbindAll();

            CreateUI();
            CreateBackgroundMusic();
            
            GP_Game.GameReady();
            
            var exitSceneSignal = new ReactiveProperty<int>();
            _uiMainMenuRoot.Bind(exitSceneSignal, mainMenuContainer);
            
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

        private void CreateUI()
        {
            var uiRoot = _mainMenuContainer.Resolve<UIRootView>();
            _uiMainMenuRoot = Instantiate(_uiMainMenuRootPrefab);
            uiRoot.AttachSceneUI(_uiMainMenuRoot.gameObject);
        }

        private void CreateBackgroundMusic()
        {
            var gameStateProvider = _mainMenuContainer.Resolve<IGameStateProvider>();
            var audioClipsConfig = _mainMenuContainer.Resolve<AudioClipsConfig>();
            var backgroundMusic = Instantiate(_backgroundMusicPrefab);
            backgroundMusic.Init(gameStateProvider, audioClipsConfig);
            DontDestroyOnLoad(backgroundMusic.gameObject);
        }
    }
}
