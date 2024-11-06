using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.MainMenu
{
    public class LevelEntryView : MonoBehaviour
    {
        [SerializeField] private GameObject _defaultStage;
        [SerializeField] private GameObject _lockStage;
        [SerializeField] private GameObject _completeStage;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _defaultStageLevelText;
        [SerializeField] private TextMeshProUGUI _completeStageLevelText;
        [SerializeField] private Image[] _starImages;
        [SerializeField] private Color _oneStarColor;
        [SerializeField] private Color _twoStarsColor;
        [SerializeField] private Color _threeStarsColor;

        public event Action OnButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeButtonEvent);
        }

        private void InvokeButtonEvent() => OnButtonClicked?.Invoke();

        public void SetActiveDefaultStage(bool activeState) => _defaultStage.gameObject.SetActive(activeState);
        public void SetActiveLockStage(bool activeState) => _lockStage.gameObject.SetActive(activeState);
        public void SetActiveCompleteStage(bool activeState) => _completeStage.gameObject.SetActive(activeState);
        public void SetLevelText(string text)
        {
            _defaultStageLevelText.text = text;
            _completeStageLevelText.text = text;
        }
        public void SetStars(int count)
        {
            Color starsColor = Color.black;
            switch (count)
            {
                case 1:
                    starsColor = _oneStarColor;
                    break;
                case 2:
                    starsColor = _twoStarsColor;
                    break;
                case 3:
                    starsColor = _threeStarsColor;
                    break;
            }
            
            for (int i = 0; i < count; i++)
            {
                var currentImage = _starImages[i];
                currentImage.gameObject.SetActive(true);
                currentImage.color = starsColor;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeButtonEvent);
        }
    }
}
