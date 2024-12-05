using TowerMergeTD.Game.Audio;

namespace TowerMergeTD.Game.UI
{
    public class LevelsPanelViewAdapter
    {
        private readonly LevelsPanelView _view;
        private readonly AudioPlayer _audioPlayer;

        public LevelsPanelViewAdapter(LevelsPanelView view, AudioPlayer audioPlayer)
        {
            _view = view;
            _audioPlayer = audioPlayer;

            _view.OnCloseButtonClicked += HandleLevelPanelCloseButtonClicked;
        }

        private void HandleLevelPanelCloseButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _view.Hide();
        }
    }
}