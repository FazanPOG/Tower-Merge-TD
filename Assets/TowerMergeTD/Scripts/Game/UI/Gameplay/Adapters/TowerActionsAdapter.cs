using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsAdapter
    {
        private readonly TowerActionsView _view;
        private readonly InputHandler _inputHandler;
        private readonly TowerFactory _towerFactory;
        private readonly MapCoordinator _mapCoordinator;
        private readonly PlayerMoneyProxy _playerMoneyProxy;

        private Vector2 _currentPosition;
        private TowerObject _currentClickedTower;
        
        public TowerActionsAdapter(
            TowerActionsView view, 
            InputHandler inputHandler, 
            TowerFactory towerFactory, 
            MapCoordinator mapCoordinator, 
            PlayerMoneyProxy playerMoneyProxy)
        {
            _view = view;
            _inputHandler = inputHandler;
            _towerFactory = towerFactory;
            _mapCoordinator = mapCoordinator;
            _playerMoneyProxy = playerMoneyProxy;

            _inputHandler.OnMouseClicked += OnMouseClicked;
            _inputHandler.OnMouseDrag += OnMouseDrag;
            _view.OnCreateTowerButtonClicked += CreateTower;
            _view.OnSellTowerButtonClicked += SellTower;
        }

        private void OnMouseClicked()
        {
            if(_view.IsMouseOver)
                return;

            _currentClickedTower = null;
            Vector3 mouseWorldPosition = _inputHandler.GetMouseWorldPosition();
            Vector2 cellCenterPosition = _mapCoordinator.GetCellCenterPosition(TilemapType.Base, mouseWorldPosition);
            
            if (_mapCoordinator.HasTowerInCell(out TowerObject towerObject))
            {
                _currentClickedTower = towerObject;
                TowerClickedState(cellCenterPosition);
            }
            else if (_mapCoordinator.CanPlaceTower(mouseWorldPosition))
            {
                MapClickedState(cellCenterPosition);
            }
            else
            {
                _view.Hide();
            }
        }

        private void OnMouseDrag()
        {
            if (_view.gameObject.activeSelf)
                _view.Hide();
        }

        private void CreateTower(TowerType towerType)
        {
            int cost = _towerFactory.GetCreateCost(towerType);
            
            if (cost <= _playerMoneyProxy.Money.CurrentValue)
            {
                _towerFactory.Create(towerType, _currentPosition, 1);
                _playerMoneyProxy.Money.Value -= cost;
            }
            
            _view.Hide();
        }

        private void SellTower()
        {
            if(_currentClickedTower == null)
                throw new MissingReferenceException("Missing tower to sell");

            int sellPrice = calculateSellPrice();
            
            _playerMoneyProxy.Money.Value += sellPrice;
            _currentClickedTower.DestroySelf();
            _view.Hide();

            int calculateSellPrice()
            {
                int cost = _towerFactory.GetCreateCost(_currentClickedTower.Type);
                int sellKoeff = 2;
                int result = (cost * _currentClickedTower.Level) / sellKoeff;
                return result;
            }
        }

        private void TowerClickedState(Vector2 cellCenterPosition)
        {
            _currentPosition = cellCenterPosition;

            _view.SetActiveCreateTowerButtons(false);
            _view.SetActiveSellButton(true);
            _view.Show();
            _view.UpdatePosition(cellCenterPosition);
        }

        private void MapClickedState(Vector2 cellCenterPosition)
        {
            _currentPosition = cellCenterPosition;

            _view.SetActiveCreateTowerButtons(true);
            _view.SetActiveSellButton(false);
            _view.Show();
            _view.UpdatePosition(cellCenterPosition);
        }
    }
}