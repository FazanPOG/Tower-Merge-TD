using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PlayerGemsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gemsValueText;
        [SerializeField] private Button _addButton;

        public event Action OnAddButtonClicked;
        
        private void OnEnable()
        {
            _addButton.onClick.AddListener(() => OnAddButtonClicked?.Invoke());
        }

        public void SetGemsValueText(string text) => _gemsValueText.text = text;
        
        private void OnDisable()
        {
            _addButton.onClick.RemoveAllListeners();
        }
    }
}