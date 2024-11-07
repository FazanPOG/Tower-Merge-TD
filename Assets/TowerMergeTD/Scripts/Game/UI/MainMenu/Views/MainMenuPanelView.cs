using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class MainMenuPanelView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnPlayButtonClicked;
        public event Action OnShopButtonClicked;
        public event Action OnSettingsButtonClicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopButtonClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
        
        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();
            _shopButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }
    }
}