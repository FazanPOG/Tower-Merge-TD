using System;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowersListView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _gunTowerButton;
        [SerializeField] private Button _rocketTowerButton;

        private Camera _mainCamera;
        private Canvas _parentCanvas;
        private RectTransform _rectTransform;

        public bool CanDisable { get; set; }
        public bool IsMouseOver { get; private set; }
        
        public event Action OnGunTowerButtonClicked;
        public event Action OnRocketTowerButtonClicked;
        
        private void Start()
        {
            _mainCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            _parentCanvas = GetComponentInParent<Canvas>();
            CanDisable = true;
            
            Hide();
        }
        
        private void OnEnable()
        {
            _gunTowerButton.onClick.AddListener(() => OnGunTowerButtonClicked?.Invoke());
            _rocketTowerButton.onClick.AddListener(() => OnRocketTowerButtonClicked?.Invoke());
        }

        public void OnPointerEnter(PointerEventData eventData) => IsMouseOver = true;
        
        public void OnPointerExit(PointerEventData eventData) => IsMouseOver = false;
        
        public void Show() => gameObject.SetActive(true);

        public void Hide()
        {
            if (CanDisable)
            {
                IsMouseOver = false;
                gameObject.SetActive(false);
            }
        }

        public void SetButtonInteractable(TowerType towerType, bool canInteract)
        {
            switch (towerType)
            {
                case TowerType.Gun:
                    _gunTowerButton.interactable = canInteract;
                    break;
                
                case TowerType.Rocket:
                    _rocketTowerButton.interactable = canInteract;
                    break;
            }
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
            _gunTowerButton.onClick.RemoveAllListeners();
            _rocketTowerButton.onClick.RemoveAllListeners();
        }
    }
}