using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowerInfoPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Button _closeButton;

        public event Action OnCloseButtonClicked;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetTitleText(string text) => _titleText.text = text;
        public void SetInfoText(string text) => _infoText.text = text;
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}