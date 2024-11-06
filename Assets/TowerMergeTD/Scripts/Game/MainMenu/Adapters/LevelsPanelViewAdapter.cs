namespace TowerMergeTD.Game.MainMenu
{
    public class LevelsPanelViewAdapter
    {
        private readonly LevelsPanelView _view;

        public LevelsPanelViewAdapter(LevelsPanelView view)
        {
            _view = view;
            
            _view.OnCloseButtonClicked += HandleLevelPanelCloseButtonClicked;
        }

        private void HandleLevelPanelCloseButtonClicked()
        {
            _view.Hide();
        }
    }
}