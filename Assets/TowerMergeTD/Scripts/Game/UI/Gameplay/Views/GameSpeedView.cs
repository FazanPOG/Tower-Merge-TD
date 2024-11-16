using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class GameSpeedView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _speedText;

        public event Action OnSpeedButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnSpeedButtonClicked?.Invoke());
        }

        public void SetSpeedText(string text) => _speedText.text = text;
        
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}