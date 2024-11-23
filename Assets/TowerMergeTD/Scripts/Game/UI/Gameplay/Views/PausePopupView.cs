using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class PausePopupView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TextMeshProUGUI _pauseText;
        [SerializeField] private TextMeshProUGUI _continueText;
        [SerializeField] private TextMeshProUGUI _restartText;
        [SerializeField] private TextMeshProUGUI _exitText;

        public event Action OnContinueButtonClicked;
        public event Action OnRestartButtonClicked;
        public event Action OnExitButtonClicked;
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetPauseText(string text) => _pauseText.text = text;
        public void SetContinueText(string text) => _continueText.text = text;
        public void SetRestartText(string text) => _restartText.text = text;
        public void SetExitText(string text) => _exitText.text = text;
        
        private void OnDisable()
        {
            _continueButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}