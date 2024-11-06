using System;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.UI
{
    public class PlayerMoneyViewAdapter
    {
        private readonly PlayerMoneyView _view;
        private readonly PlayerMoneyProxy _playerMoneyProxy;

        private IDisposable _disposable;

        public PlayerMoneyViewAdapter(PlayerMoneyView view, PlayerMoneyProxy playerMoneyProxy)
        {
            _view = view;
            _playerMoneyProxy = playerMoneyProxy;

            _playerMoneyProxy.Money.Subscribe(UpdateView);
        }

        private void UpdateView(int money)
        {
            _view.UpdateText($"Money: {money}");
        }
    }
}