using System;
using R3;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.UI
{
    public class PlayerHealthViewAdapter
    {
        private readonly PlayerHealthView _view;
        private readonly PlayerHealthProxy _playerHealthProxy;

        public PlayerHealthViewAdapter(PlayerHealthView view, PlayerHealthProxy playerHealthProxy)
        {
            _view = view;
            _playerHealthProxy = playerHealthProxy;
            
            _playerHealthProxy.Health.Subscribe(UpdateView);
        }

        private void UpdateView(int health)
        {
            _view.UpdateText($"{Math.Max(0, health)}");
        }
    }
}