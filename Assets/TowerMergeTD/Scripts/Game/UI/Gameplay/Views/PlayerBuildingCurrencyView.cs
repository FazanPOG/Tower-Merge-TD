using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PlayerBuildingCurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Button _plusButton;

        public event Action OnPlusButtonClicked;

        private void OnEnable()
        {
            _plusButton.onClick.AddListener(() => OnPlusButtonClicked?.Invoke());
        }
        
        public void SetValueText(string text) => _valueText.text = text;
        
        private void OnDisable()
        {
            _plusButton.onClick.RemoveAllListeners();
        }
    }
}