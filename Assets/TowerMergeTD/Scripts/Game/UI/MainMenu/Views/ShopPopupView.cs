using System;
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
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveAllListeners();
            _towerShopButton.onClick.RemoveAllListeners();
            _coinShopButton.onClick.RemoveAllListeners();
            _gemShopButton.onClick.RemoveAllListeners();
        }
    }
}