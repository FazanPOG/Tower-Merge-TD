using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Button _plusButton;

        public event Action OnPlusButtonClicked;

        private void OnEnable()
        {
            _plusButton.onClick.AddListener(() => OnPlusButtonClicked?.Invoke());
        }

        public void UpdateText(string text) => _healthText.text = text;

        private void OnDisable()
        {
            _plusButton.onClick.RemoveAllListeners();
        }
    }
}