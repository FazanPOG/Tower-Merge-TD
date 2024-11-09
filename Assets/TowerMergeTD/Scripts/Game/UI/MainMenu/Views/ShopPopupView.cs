using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class ShopPopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _towerShopButton;
        [SerializeField] private Button _coinShopButton;
        [SerializeField] private Button _gemShopButton;
        [SerializeField] private TextMeshProUGUI _towerText;
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _gemText;
        [SerializeField] private Color _enabledTextColor;
        [SerializeField] private Color _disabledTextColor;
        [SerializeField] private Image _towerButtonImage;
        [SerializeField] private Image _coinButtonImage;
        [SerializeField] private Image _gemButtonImage;
        [SerializeField] private Image _towerImage;
        [SerializeField] private Image _coinImage;
        [SerializeField] private Image _gemImage;
        [SerializeField] private Sprite _enableButtonSprite;
        [SerializeField] private Sprite _disableButtonSprite;
        [SerializeField] private Sprite _towerEnableSprite;
        [SerializeField] private Sprite _towerDisableSprite;
        [SerializeField] private Sprite _coinEnableSprite;
        [SerializeField] private Sprite _coinDisableSprite;
        [SerializeField] private Sprite _gemEnableSprite;
        [SerializeField] private Sprite _gemDisableSprite;
        
        public event Action OnCloseButtonClicked;
        public event Action OnTowerShopButtonClicked;
        public event Action OnCoinShopButtonClicked;
        public event Action OnGemShopButtonClicked;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            _towerShopButton.onClick.AddListener(() => OnTowerShopButtonClicked?.Invoke());
            _coinShopButton.onClick.AddListener(() => OnCoinShopButtonClicked?.Invoke());
            _gemShopButton.onClick.AddListener(() => OnGemShopButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void SetActiveTowerView(bool activeState)
        {
            _towerButtonImage.sprite = activeState ? _enableButtonSprite : _disableButtonSprite;
            _towerImage.sprite = activeState ? _towerEnableSprite : _towerDisableSprite;
            _towerText.color = activeState ? _enabledTextColor : _disabledTextColor;
        }
        
        public void SetActiveCoinView(bool activeState)
        {
            _coinButtonImage.sprite = activeState ? _enableButtonSprite : _disableButtonSprite;
            _coinImage.sprite = activeState ? _coinEnableSprite : _coinDisableSprite;
            _coinText.color = activeState ? _enabledTextColor : _disabledTextColor;
        }
        
        public void SetActiveGemView(bool activeState)
        {
            _gemButtonImage.sprite = activeState ? _enableButtonSprite : _disableButtonSprite;
            _gemImage.sprite = activeState ? _gemEnableSprite : _gemDisableSprite;
            _gemText.color = activeState ? _enabledTextColor : _disabledTextColor;
        }
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _towerShopButton.onClick.RemoveAllListeners();
            _coinShopButton.onClick.RemoveAllListeners();
            _gemShopButton.onClick.RemoveAllListeners();
        }
    }
}