using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class SettingsPopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _settingsText;
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private TextMeshProUGUI _musicText;

        public event Action OnCloseButtonClicked;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
        }

        public void SetSettingsText(string text) => _settingsText.text = text;
        public void SetSoundText(string text) => _soundText.text = text;
        public void SetMusicText(string text) => _musicText.text = text;
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}