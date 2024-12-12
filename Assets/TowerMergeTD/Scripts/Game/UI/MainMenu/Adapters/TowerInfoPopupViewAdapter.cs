using TowerMergeTD.Game.Audio;

namespace TowerMergeTD.Game.UI
{
    public class TowerInfoPopupViewAdapter
    {
        private readonly TowerInfoPopupView _infoPopupView;
        private readonly AudioPlayer _audioPlayer;

        public TowerInfoPopupViewAdapter(TowerInfoPopupView infoPopupView, AudioPlayer audioPlayer)
        {
            _infoPopupView = infoPopupView;
            _audioPlayer = audioPlayer;

            _infoPopupView.OnCloseButtonClicked += OnCloseButtonClicked;
        }

        private void OnCloseButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _infoPopupView.Hide();
        }
    }
}