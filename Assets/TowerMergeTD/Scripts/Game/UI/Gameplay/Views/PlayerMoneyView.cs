using System;
using R3;
using TMPro;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class PlayerMoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private PlayerMoneyProxy _playerMoneyProxy;
        private IDisposable _disposable;
        
        public void Init(PlayerMoneyProxy playerMoneyProxy)
        {
            _playerMoneyProxy = playerMoneyProxy;
            
            _disposable = _playerMoneyProxy.Money.Subscribe(UpdateText);
        }

        private void UpdateText(int money)
        {
            _moneyText.text = $"Money: {money}";
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}
