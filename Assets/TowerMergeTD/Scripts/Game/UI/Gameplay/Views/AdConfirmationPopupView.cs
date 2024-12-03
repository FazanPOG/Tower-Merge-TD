using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class AdConfirmationPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private TextMeshProUGUI _closeButtonText;
        [SerializeField] private TextMeshProUGUI _waitAdText;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Sprite _healthSprite;
        [SerializeField] private Sprite _buildingCurrencySprite;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _confirmButton;
        
        public event Action OnCloseButtonClicked;
        public event Action OnConfirmButtonClicked;
        
        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _confirmButton.onClick.AddListener(() => OnConfirmButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetActiveRewardImage(bool activeState) => _rewardImage.gameObject.SetActive(activeState);
        public void SetActiveConfirmButton(bool activeState) => _confirmButton.gameObject.SetActive(activeState);
        public void SetActiveRewardText(bool activeState) => _rewardText.gameObject.SetActive(activeState);
        public void SetActiveWaitAdText(bool activeState) => _waitAdText.gameObject.SetActive(activeState);
        public void SetWaitAdText(string text) => _waitAdText.text = text;
        public void SetTitleText(string text) => _titleText.text = text;
        public void SetCloseButtonText(string text) => _closeButtonText.text = text;
        public void SetRewardText(string text) => _rewardText.text = text;
        public void SetHealthSprite() => _rewardImage.sprite = _healthSprite;
        public void SetBuildingCurrencySprite() => _rewardImage.sprite = _buildingCurrencySprite;
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _confirmButton.onClick.RemoveAllListeners();
        }
    }
}