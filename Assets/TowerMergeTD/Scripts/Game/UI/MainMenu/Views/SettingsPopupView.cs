using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class SettingsPopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        public event Action OnCloseButtonClicked;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}