using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class LevelCompletePopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _completeText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _gemsText;
        
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private TextMeshProUGUI _timerValueText;
        [SerializeField] private TextMeshProUGUI _goldValueText;
        [SerializeField] private TextMeshProUGUI _gemsValueText;
        [SerializeField] private Image[] _starImages;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextLevelButton;

        public event Action OnHomeButtonClicked;
        public event Action OnRestartButtonClicked;
        public event Action OnNextLevelButtonClicked;

        private void OnEnable()
        {
            _homeButton.onClick.AddListener(() => OnHomeButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
            _nextLevelButton.onClick.AddListener(() => OnNextLevelButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);

        public void ShowNextLevelButton() => _nextLevelButton.gameObject.SetActive(true);
        public void HideNextLevelButton() => _nextLevelButton.gameObject.SetActive(false);

        public void SetCompleteText(string text) => _completeText.text = text;
        public void SetScoreText(string text) => _scoreText.text = text;
        public void SetTimeText(string text) => _timeText.text = text;
        public void SetCoinText(string text) => _coinText.text = text;
        public void SetGemsText(string text) => _gemsText.text = text;
        
        public void SetScoreValueText(string text) => _scoreValueText.text = text;
        public void SetTimerValueText(string text) => _timerValueText.text = text;
        public void SetGoldValueText(string text) => _goldValueText.text = text;
        public void SetGemsValueText(string text) => _gemsValueText.text = text;

        public void SetStars(int count)
        {
            foreach (var image in _starImages)
                image.gameObject.SetActive(false);
            
            for (int i = count - 1; i >= 0; i--)
                _starImages[i].gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            _homeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _nextLevelButton.onClick.RemoveAllListeners();
        }
    }
}