using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsAdapter : IPauseHandler
    {
        private readonly TowerActionsView _view;
        private readonly IInput _input;
        private readonly TowerFactory _towerFactory;
        private readonly MapCoordinator _mapCoordinator;
        private readonly PlayerBuildingCurrencyProxy _buildingCurrencyProxy;
        private readonly IPauseService _pauseService;

        private Vector2 _currentPosition;
        private TowerObject _currentClickedTower;
        private bool _canInteract;

        public TowerActionsAdapter(
            TowerActionsView view, 
            IInput input, 
            TowerFactory towerFactory, 
            MapCoordinator mapCoordinator, 
            PlayerBuildingCurrencyProxy buildingCurrencyProxy,
            IPauseService pauseService)
        {
            _view = view;
            _input = input;
            _towerFactory = towerFactory;
            _mapCoordinator = mapCoordinator;
            _buildingCurrencyProxy = buildingCurrencyProxy;
            _pauseService = pauseService;

            _canInteract = true;
            Register();
        }

        private void Register()
        {
            _input.OnClicked += OnClicked;
            _input.OnDragWithThreshold += OnDrag;
            _view.OnCreateTowerButtonClicked += CreateTower;
            _view.OnSellTowerButtonClicked += SellTower;
            _pauseService.Register(this);
        }

        public void HandlePause(bool isPaused)
        {
            _canInteract = !isPaused;
            
            if(isPaused)
                _view.Hide();
        }

        private void OnClicked()
        {
            if(_canInteract == false) return;
            if(_view.IsMouseOver) return;

            _currentClickedTower = null;
            Vector3 mouseWorldPosition = _input.GetInputWorldPosition();
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

        private void OnDrag(Vector2 _)
        {
            if (_view.gameObject.activeSelf)
                _view.Hide();
        }

        private void CreateTower(TowerType towerType)
        {
            int cost = _towerFactory.GetCreateCost(towerType);
            
            if (cost <= _buildingCurrencyProxy.BuildingCurrency.CurrentValue)
            {
                _towerFactory.Create(towerType, _currentPosition, 1);
                _buildingCurrencyProxy.BuildingCurrency.Value -= cost;
            }
            
            _view.Hide();
        }

        private void SellTower()
        {
            if(_currentClickedTower == null)
                throw new MissingReferenceException("Missing tower to sell");

            int sellPrice = calculateSellPrice();
            
            _buildingCurrencyProxy.BuildingCurrency.Value += sellPrice;
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