using R3;
using UnityEngine.UI;

namespace TowerMergeTD.Game.Gameplay
{
    public class ClickButtonTutorialAction : ITutorialAction
    {
        private readonly Button _button;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public ClickButtonTutorialAction(Button button)
        {
            _button = button;
        }

        public void StartAction()
        {
            _button.onClick.AddListener(() => _isComplete.Value = true);
        }
    }
}