using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

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
            _view.UpdateText($"{health}");
        }
    }
}