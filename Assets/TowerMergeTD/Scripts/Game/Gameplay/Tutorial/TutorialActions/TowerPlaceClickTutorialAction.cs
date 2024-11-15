using R3;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerPlaceClickTutorialAction : ITutorialAction
    {
        private readonly Vector2 _tileWorldPosition;
        private readonly IInput _input;
        private readonly MapCoordinator _mapCoordinator;
        private readonly TowersListView _towersListView;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public TowerPlaceClickTutorialAction(Vector2 tileWorldPosition, IInput input, MapCoordinator mapCoordinator, TowersListView towersListView)
        {
            _tileWorldPosition = tileWorldPosition;
            _input = input;
            _mapCoordinator = mapCoordinator;
            _towersListView = towersListView;
        }
        
        public void StartAction()
        {
            _input.OnClicked += OnClicked;
        }

        private void OnClicked()
        {
            Vector3 mouseWorldPosition = _input.GetInputWorldPosition();
            Vector2 mouse2DWorldPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
            
            if (canComplete())
                _isComplete.Value = true;
            else if(_isComplete.CurrentValue == false)
                _towersListView.Hide();

            bool canComplete()
            {
                return _mapCoordinator.CanPlaceTower(mouseWorldPosition)
                       && _mapCoordinator.HasTowerInCell(mouseWorldPosition, out TowerObject _) == false
                       && _mapCoordinator.GetTileWorldPosition(mouse2DWorldPosition) == _tileWorldPosition;
            }
        }
        
        public void Dispose()
        {
            _isComplete?.Dispose();
        }
    }
}