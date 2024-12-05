using System;
using R3;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerCoinsViewAdapter
    {
        private readonly PlayerCoinsView _view;
        private readonly ShopPopupView _shopView;
        private readonly ShopPopupViewAdapter _shopAdapter;
        private readonly AudioPlayer _audioPlayer;

        private IDisposable _disposable;

        public PlayerCoinsViewAdapter(
            PlayerCoinsView view, 
            PlayerCoinsProxy playerCoinsProxy, 
            ShopPopupView shopView, 
            ShopPopupViewAdapter shopAdapter,
            AudioPlayer audioPlayer)
        {
            _view = view;
            _shopView = shopView;
            _shopAdapter = shopAdapter;
            _audioPlayer = audioPlayer;

            _view.OnAddButtonClicked += OnAddButtonClicked;
            playerCoinsProxy.Coins.Subscribe(UpdateView);
        }

        private void OnAddButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _shopView.Show();
            _shopAdapter.ShowCoinShop();
        }

        private void UpdateView(int money)
        {
            _view.SetCoinsValueText(money.ToString("N0"));
        }
    }
}