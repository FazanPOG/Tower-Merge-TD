using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public class MergeTutorialAction : ITutorialAction
    {
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;
        
        public void StartAction()
        {
            MergeHandler.OnMerged += IsCompleted;
        }

        private void IsCompleted()
        {
            _isComplete.Value = true;
        }
        
        public void Dispose()
        {
            MergeHandler.OnMerged -= IsCompleted;
            _isComplete?.Dispose();
        }
    }
}