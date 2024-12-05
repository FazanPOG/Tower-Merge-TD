using R3;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerGemsViewAdapter
    {
        private readonly PlayerGemsView _view;
        private readonly ShopPopupView _shopView;
        private readonly ShopPopupViewAdapter _shopAdapter;
        private readonly AudioPlayer _audioPlayer;

        public PlayerGemsViewAdapter(
        PlayerGemsView view, 
        PlayerGemsProxy gemsProxy, 
        ShopPopupView shopView, 
        ShopPopupViewAdapter shopAdapter,
        AudioPlayer audioPlayer)
        {
            _view = view;
            _shopView = shopView;
            _shopAdapter = shopAdapter;
            _audioPlayer = audioPlayer;

            _view.OnAddButtonClicked += OnAddButtonClicked;
            gemsProxy.Gems.Subscribe(UpdateView);
        }

        private void OnAddButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _shopView.Show();
            _shopAdapter.ShowGemShop();
        }

        private void UpdateView(int gems)
        {
            _view.SetGemsValueText(gems.ToString("N0"));
        }
    }
}