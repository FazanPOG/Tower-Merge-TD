using System;
using R3;
using TMPro;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;

        private PlayerHealthProxy _playerHealth;
        private IDisposable _disposable;
        
        public void Init(PlayerHealthProxy playerHealth)
        {
            _playerHealth = playerHealth;
            
            _disposable = _playerHealth.Health.Subscribe(newValue => { UpdateText($"HP: {newValue}"); });
        }

        private void UpdateText(string text) => _healthText.text = text;
        
        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}