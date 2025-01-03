﻿using System;
using TMPro;
using TowerMergeTD.Game.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class ShopItemView : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private ShopItemConfig _shopItemConfig;
        
        [Header("View")]
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _itemBackgroundImage;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Sprite _adSprite;
        [SerializeField] private Color _currencyImageADColor;
        [SerializeField] private RectTransform _bestLabel;
        [SerializeField] private TextMeshProUGUI _bestLabelText;
        [SerializeField] private TextMeshProUGUI _bonusText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _infoButton;

        public event Action OnBuyButtonClicked;
        public event Action OnInfoButtonClicked;

        public ShopItemConfig ShopItemConfig => _shopItemConfig;
        
        private void OnEnable()
        {
            _buyButton.onClick.AddListener(() => OnBuyButtonClicked?.Invoke());
            _infoButton.onClick.AddListener(() => OnInfoButtonClicked?.Invoke());
        }

        public void SetInfoButtonActiveState(bool activeState) => _infoButton.gameObject.SetActive(activeState);
        public void SetButtonInteractable(bool canInteract) => _buyButton.interactable = canInteract;
        public void SetButtonText(string text) => _priceText.text = text;
        public void SetButtonTextActiveState(bool activeState) => _priceText.gameObject.SetActive(activeState);
        public void SetValueText(string text) => _valueText.text = text;
        public void SetItemIconSprite(Sprite sprite) => _itemImage.sprite = sprite;

        public void SetActiveBestLabel(bool activeState) => _bestLabel.gameObject.SetActive(activeState);
        public void SetBestLabelText(string text) => _bestLabelText.text = text;
        public void SetActiveBonusText(bool activeState) => _bonusText.gameObject.SetActive(activeState);
        public void SetBonusText(string text) => _bonusText.text = text;
        
        public void SetItemIconBackgroundColor(Color color) => _itemBackgroundImage.color = color;

        public void SetButtonSprite(Sprite sprite)
        {
            _currencyImage.sprite = sprite;
            _currencyImage.color = Color.white;
        }

        public void SetButtonAdSprite()
        {
            _currencyImage.sprite = _adSprite;
            _currencyImage.color = _currencyImageADColor;
        }

        public void SetButtonImageActiveState(bool activeState) => _currencyImage.gameObject.SetActive(activeState);

        private void OnDisable()
        {
            _buyButton.onClick.RemoveAllListeners();
            _infoButton.onClick.RemoveAllListeners();
        }
    }
}