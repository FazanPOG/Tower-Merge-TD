using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class LevelLockPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNumberText; 
        [SerializeField] private Button[] _closeButtons;

        public event Action OnCloseButtonClicked;
        
        private void OnEnable()
        {
            foreach (var button in _closeButtons)
                button.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetLevelNumberText(string text) => _levelNumberText.text = text;
        
        private void OnDisable()
        {
            foreach (var button in _closeButtons)
                button.onClick.RemoveAllListeners();
        }
    }
}