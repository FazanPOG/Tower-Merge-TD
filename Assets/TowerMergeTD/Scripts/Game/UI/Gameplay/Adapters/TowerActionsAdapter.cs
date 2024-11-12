using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsAdapter : IPauseHandler
    {
        private readonly TowerSellView _towerSellView;
        private readonly TowersListView _towersListView;
        private readonly IInput _input;
        private readonly TowerFactory _towerFactory;
        private readonly MapCoordinator _mapCoordinator;
        private readonly PlayerBuildingCurrencyProxy _buildingCurrencyProxy;
        private readonly IPauseService _pauseService;

        private Vector2 _currentPosition;
        private TowerObject _currentClickedTower;
        private bool _canInteract;

        public TowerActionsAdapter(
            TowerSellView towerSellView,
            TowersListView towersListView,
            IInput input, 
            TowerFactory towerFactory, 
            MapCoordinator mapCoordinator, 
            PlayerBuildingCurrencyProxy buildingCurrencyProxy,
            IPauseService pauseService)
        {
            _towerSellView = towerSellView;
            _towersListView = towersListView;
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
            _towersListView.OnTowerButtonClicked += CreateTower;
            _towerSellView.OnSellTowerButtonClicked += SellTower;
            _pauseService.Register(this);
        }

        public void HandlePause(bool isPaused)
        {
            _canInteract = !isPaused;
            
            if(isPaused)
                _towerSellView.Hide();
        }

        private void OnClicked()
        {
            if(_canInteract == false) return;
            if(_towerSellView.IsMouseOver) return;
            if(_towersListView.IsMouseOver) return;

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
                _towerSellView.Hide();
                _towersListView.Hide();
            }
        }

        private void OnDrag(Vector2 _)
        {
            if (_towerSellView.gameObject.activeSelf)
                _towerSellView.Hide();
            
            if(_towersListView.gameObject.activeSelf)
                _towersListView.Hide();
        }

        private void CreateTower(TowerType towerType)
        {
            int cost = _towerFactory.GetCreateCost(towerType);
            
            if (cost <= _buildingCurrencyProxy.BuildingCurrency.CurrentValue)
            {
                _towerFactory.Create(towerType, _currentPosition, 1);
                _buildingCurrencyProxy.BuildingCurrency.Value -= cost;
            }
            
            _towersListView.Hide();
        }

        private void SellTower()
        {
            if(_currentClickedTower == null)
                throw new MissingReferenceException("Missing tower to sell");

            int sellPrice = calculateSellPrice();
            
            _buildingCurrencyProxy.BuildingCurrency.Value += sellPrice;
            _currentClickedTower.DestroySelf();
            _towerSellView.Hide();

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

            _towerSellView.Show();
            _towerSellView.UpdatePosition(cellCenterPosition);
        }

        private void MapClickedState(Vector2 cellCenterPosition)
        {
            _currentPosition = cellCenterPosition;

            _towersListView.Show();
            _towersListView.UpdatePosition(cellCenterPosition);
        }
    }
}