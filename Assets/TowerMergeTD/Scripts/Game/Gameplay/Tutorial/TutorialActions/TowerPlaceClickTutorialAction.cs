using R3;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerPlaceClickTutorialAction : ITutorialAction
    {
        private readonly IInput _input;
        private readonly MapCoordinator _mapCoordinator;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public TowerPlaceClickTutorialAction(IInput input, MapCoordinator mapCoordinator)
        {
            _input = input;
            _mapCoordinator = mapCoordinator;
        }
        
        public void StartAction()
        {
            _input.OnClicked += OnClicked;
        }

        private void OnClicked()
        {
            Vector3 mouseWorldPosition = _input.GetInputWorldPosition();

            if (_mapCoordinator.CanPlaceTower(mouseWorldPosition))
                _isComplete.Value = true;
        }
        
        public void Dispose()
        {
            _isComplete?.Dispose();
        }
    }
}