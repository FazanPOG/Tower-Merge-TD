using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private PlayerMoneyView _playerMoneyView;
        [SerializeField] private TowerActionsView _towerActionsView;

        private DiContainer _container;
        private PlayerHealthProxy _playerHealthProxy;
        private PlayerMoneyProxy _playerMoneyProxy;

        private Subject<Unit> _exitSceneSignalBus;

        public void HandleGoToMainMenuButtonClicked()
        {
            _exitSceneSignalBus?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalBus, DiContainer container)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _container = container;

            _playerHealthProxy = _container.Resolve<PlayerHealthProxy>();
            _playerMoneyProxy = _container.Resolve<PlayerMoneyProxy>();
            
            BindAdapters();
            BindViews();
        }

        private void BindAdapters()
        {
            var inputHandler = _container.Resolve<InputHandler>();
            var towerFactory = _container.Resolve<TowerFactory>();
            var mapCoordinator = _container.Resolve<MapCoordinator>();
            
            new TowerActionsAdapter(_towerActionsView, inputHandler, towerFactory, mapCoordinator, _playerMoneyProxy);
        }
        
        private void BindViews()
        {
            _playerHealthView.Init(_playerHealthProxy);
            _playerMoneyView.Init(_playerMoneyProxy);
        }
    }
}
