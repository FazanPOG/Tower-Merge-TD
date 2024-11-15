using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowerSellView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _sellTowerButton;

        private Camera _mainCamera;
        private Canvas _parentCanvas;
        private RectTransform _rectTransform;

        public bool CanEnabled { get; set; } = true;

        public bool IsMouseOver { get; private set; }
        
        public event Action OnSellTowerButtonClicked;

        private void Start()
        {
            _mainCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            _parentCanvas = GetComponentInParent<Canvas>();
            
            Hide();
        }

        private void OnEnable()
        {
            _sellTowerButton.onClick.AddListener(() => OnSellTowerButtonClicked?.Invoke());
        }
        
        public void OnPointerEnter(PointerEventData eventData) => IsMouseOver = true;
        
        public void OnPointerExit(PointerEventData eventData) => IsMouseOver = false;

        public void Show()
        {
            if(CanEnabled)
                gameObject.SetActive(true);
        }

        public void Hide()
        {
            IsMouseOver = false;
            gameObject.SetActive(false);
        }

        public void UpdatePosition(Vector2 position)
        {
            Vector2 canvasPosition = WorldToCanvasPosition(position);
            _rectTransform.anchoredPosition = canvasPosition;
        }

        private Vector2 WorldToCanvasPosition(Vector2 worldPosition)
        {
            Vector2 screenPoint = _mainCamera.WorldToScreenPoint(worldPosition);
        
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentCanvas.transform as RectTransform,
                screenPoint,
                _parentCanvas.worldCamera,
                out Vector2 canvasPosition);

            return canvasPosition;
        }

        private void OnDisable()
        {
            _sellTowerButton.onClick.RemoveAllListeners();
        }
    }
}