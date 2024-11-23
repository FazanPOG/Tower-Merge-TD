using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class MainMenuPanelView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _gameTitleText;
        [SerializeField] private TextMeshProUGUI _shopText;
        [SerializeField] private TextMeshProUGUI _settingsText;

        public event Action OnPlayButtonClicked;
        public event Action OnShopButtonClicked;
        public event Action OnSettingsButtonClicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
        }

        public void SetGameTitleText(string text) => _gameTitleText.text = text;
        public void SetShopText(string text) => _shopText.text = text;
        public void SetSettingsText(string text) => _settingsText.text = text;
        
        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }
    }
}