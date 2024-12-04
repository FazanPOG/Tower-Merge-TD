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
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;

        public event Action OnCloseButtonClicked;

        public event Action<float> OnSoundSliderChanged;
        public event Action<float> OnMusicSliderChanged;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _soundSlider.onValueChanged.AddListener((value) => OnSoundSliderChanged?.Invoke(value));
            _musicSlider.onValueChanged.AddListener((value) => OnMusicSliderChanged?.Invoke(value));
        }

        public void SetSettingsText(string text) => _settingsText.text = text;
        public void SetSoundText(string text) => _soundText.text = text;
        public void SetMusicText(string text) => _musicText.text = text;

        public void SetSoundSliderValue(float value) => _soundSlider.value = value;
        public void SetMusicSliderValue(float value) => _musicSlider.value = value;
        
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _soundSlider.onValueChanged.RemoveAllListeners();
            _musicSlider.onValueChanged.RemoveAllListeners();
        }
    }
}