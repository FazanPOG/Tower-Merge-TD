using System;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerMoneyViewAdapter
    {
        private readonly PlayerMoneyView _view;
        private readonly PlayerGoldProxy _playerGoldProxy;

        private IDisposable _disposable;

        public PlayerMoneyViewAdapter(PlayerMoneyView view, PlayerGoldProxy playerGoldProxy)
        {
            _view = view;
            _playerGoldProxy = playerGoldProxy;

            _playerGoldProxy.Gold.Subscribe(UpdateView);
        }

        private void UpdateView(int money)
        {
            _view.UpdateText($"{money}");
        }
    }
}