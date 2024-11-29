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
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Sprite _adSprite;
        [SerializeField] private Button _buyButton;

        public event Action OnBuyButtonClicked;

        public ShopItemConfig ShopItemConfig => _shopItemConfig;
        
        private void OnEnable()
        {
            _buyButton.onClick.AddListener(() => OnBuyButtonClicked?.Invoke());
        }

        public void SetPriceText(string text) => _priceText.text = text;
        public void SetPriceTextActiveState(bool activeState) => _priceText.gameObject.SetActive(activeState);
        public void SetValueText(string text) => _valueText.text = text;
        public void SetItemSprite(Sprite sprite) => _itemImage.sprite = sprite;
        public void SetCurrencySprite(Sprite sprite) => _currencyImage.sprite = sprite;
        public void SetCurrencyAdSprite() => _currencyImage.sprite = _adSprite;
        public void SetCurrencyImageActiveState(bool activeState) => _currencyImage.gameObject.SetActive(activeState);

        private void OnDisable()
        {
            _buyButton.onClick.RemoveAllListeners();
        }
    }
}