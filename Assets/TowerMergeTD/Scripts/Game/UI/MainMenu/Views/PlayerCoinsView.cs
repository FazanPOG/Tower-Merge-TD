using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PlayerCoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsValueText;
        [SerializeField] private Button _addButton;

        public event Action OnAddButtonClicked;
        
        private void OnEnable()
        {
            _addButton.onClick.AddListener(() => OnAddButtonClicked?.Invoke());
        }

        public void SetCoinsValueText(string text) => _coinsValueText.text = text;
        
        private void OnDisable()
        {
            _addButton.onClick.RemoveAllListeners();
        }
    }
}
