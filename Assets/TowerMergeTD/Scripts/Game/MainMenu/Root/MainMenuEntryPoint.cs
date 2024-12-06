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

        private DiContainer _mainMenuContainer;
        private MainMenuExitParams _mainMenuExitParams;
        private UIMainMenuRootView _uiMainMenuRoot;
        
        public Observable<MainMenuExitParams> Run(DiContainer mainMenuContainer, MainMenuEnterParams mainMenuEnterParams)
        {
            _mainMenuContainer = mainMenuContainer;
            
            _mainMenuContainer.UnbindAll();

            BindUI();
            InitAudio();
            
            GP_Game.GameReady();
            
            var exitSceneSignal = new ReactiveProperty<int>();
            _uiMainMenuRoot.Bind(exitSceneSignal, _mainMenuContainer);
            
            if(TryGetComponent(out MainMenuDebug mainMenuDebug))
                mainMenuDebug.Init(_mainMenuContainer);

            exitSceneSignal.Skip(1).Subscribe(levelIndex =>
            {
                var gameplayEnterParams = new GameplayEnterParams(levelIndex);
                _mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            });

            var exitToGameplaySceneSignal = exitSceneSignal.Select(_ => _mainMenuExitParams);
            return exitToGameplaySceneSignal;
        }

        private void BindUI()
        {
            var uiRoot = _mainMenuContainer.Resolve<UIRootView>();
            _uiMainMenuRoot = Instantiate(_uiMainMenuRootPrefab);
            uiRoot.AttachSceneUI(_uiMainMenuRoot.gameObject);
        }
        
        private void InitAudio()
        {
            var gameStateProvider = _mainMenuContainer.Resolve<IGameStateProvider>();
            var audioClipsConfig = _mainMenuContainer.Resolve<AudioClipsConfig>();
            
            _mainMenuContainer.Resolve<BackgroundMusic>().Init(gameStateProvider, audioClipsConfig.BackgroundMusic);
            _mainMenuContainer.Resolve<AudioPlayer>().Init(gameStateProvider, audioClipsConfig);
        }
    }
}
