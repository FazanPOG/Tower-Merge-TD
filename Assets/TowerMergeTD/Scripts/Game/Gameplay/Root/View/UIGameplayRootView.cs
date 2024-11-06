using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private PlayerMoneyView _playerMoneyView;
        [SerializeField] private TowerActionsView _towerActionsView;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private PausePopupView _pausePopupView;

        private DiContainer _container;
        private PlayerHealthProxy _playerHealthProxy;
        private PlayerMoneyProxy _playerMoneyProxy;

        private Subject<Unit> _exitSceneSignalBus;

        public void Bind(Subject<Unit> exitSceneSignalBus, DiContainer container)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _container = container;

            _playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            _playerMoneyProxy = _container.Resolve<PlayerMoneyProxy>();
            
            BindAdapters();
        }

        private void BindAdapters()
        {
            new PlayerMoneyViewAdapter(_playerMoneyView, _playerMoneyProxy);
            new PlayerHealthViewAdapter(_playerHealthView, _playerHealthProxy);
            
            bindTowerActions();
            bindPause();
            
            void bindTowerActions()
            {
                var inputHandler = _container.Resolve<InputHandler>();
                var towerFactory = _container.Resolve<TowerFactory>();
                var mapCoordinator = _container.Resolve<MapCoordinator>();
                var pauseService = _container.Resolve<IPauseService>();
            
                new TowerActionsAdapter(_towerActionsView, inputHandler, towerFactory, mapCoordinator, _playerMoneyProxy, pauseService);
            }

            void bindPause()
            {
                var pauseService = _container.Resolve<IPauseService>();
                
                new PausePopupViewAdapter(_pausePopupView, _pauseButton, _exitSceneSignalBus, pauseService);
            }
        }
    }
}
