using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsAdapter
    {
        private readonly TowerActionsView _view;
        private readonly InputHandler _inputHandler;
        private readonly TilemapCoordinator _tilemapCoordinator;
        private readonly PlayerMoneyProxy _playerMoneyProxy;

        public TowerActionsAdapter(TowerActionsView view, InputHandler inputHandler, TilemapCoordinator tilemapCoordinator, PlayerMoneyProxy playerMoneyProxy)
        {
            _view = view;
            _inputHandler = inputHandler;
            _tilemapCoordinator = tilemapCoordinator;
            _playerMoneyProxy = playerMoneyProxy;

            _inputHandler.OnMouseClicked += OnMouseClicked;
        }

        private void OnMouseClicked()
        {
            if(_view.gameObject.activeSelf)
                return;
            
            Vector3 mouseWorldPosition = _inputHandler.GetMouseWorldPosition();
            
            if (_tilemapCoordinator.CanPlaceTower(mouseWorldPosition))
            {
                _view.Show();
            }
        }
    }
}