namespace TowerMergeTD.Game.UI
{
    public class LevelLockPopupViewAdapter
    {
        private readonly LevelLockPopupView _levelLockPopupView;

        public LevelLockPopupViewAdapter(LevelLockPopupView levelLockPopupView)
        {
            _levelLockPopupView = levelLockPopupView;

            _levelLockPopupView.OnCloseButtonClicked += HandleOnCloseButtonClicked;
        }

        private void HandleOnCloseButtonClicked()
        {
            _levelLockPopupView.Hide();
        }
    }
}