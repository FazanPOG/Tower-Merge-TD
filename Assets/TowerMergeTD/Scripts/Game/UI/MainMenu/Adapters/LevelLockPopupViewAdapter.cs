using TowerMergeTD.Game.Audio;

namespace TowerMergeTD.Game.UI
{
    public class LevelLockPopupViewAdapter
    {
        private readonly LevelLockPopupView _levelLockPopupView;
        private readonly AudioPlayer _audioPlayer;

        public LevelLockPopupViewAdapter(LevelLockPopupView levelLockPopupView, AudioPlayer audioPlayer)
        {
            _levelLockPopupView = levelLockPopupView;
            _audioPlayer = audioPlayer;

            _levelLockPopupView.OnCloseButtonClicked += HandleOnCloseButtonClicked;
        }

        private void HandleOnCloseButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _levelLockPopupView.Hide();
        }
    }
}