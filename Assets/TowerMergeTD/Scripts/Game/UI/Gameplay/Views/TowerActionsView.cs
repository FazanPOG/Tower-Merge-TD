using System;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _createGunTowerButton;
        [SerializeField] private Button _createRocketTowerButton;
        [SerializeField] private Button _sellTowerButton;

        private Camera _mainCamera;
        private Canvas _parentCanvas;
        private RectTransform _rectTransform;

        public bool IsMouseOver { get; private set; }

        public event Action<TowerType> OnCreateTowerButtonClicked;
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
            _createGunTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Gun));
            _createRocketTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Rocket));
            _sellTowerButton.onClick.AddListener(() => OnSellTowerButtonClicked?.Invoke());
        }

        public void OnPointerEnter(PointerEventData eventData) => IsMouseOver = true;
        
        public void OnPointerExit(PointerEventData eventData) => IsMouseOver = false;

        public void Show() => gameObject.SetActive(true);

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

        public void SetActiveCreateTowerButtons(bool activeState)
        {
            _createGunTowerButton.gameObject.SetActive(activeState); 
            _createRocketTowerButton.gameObject.SetActive(activeState);
        }

        public void SetActiveSellButton(bool activeState) => _sellTowerButton.gameObject.SetActive(activeState);

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
            _createGunTowerButton.onClick.RemoveAllListeners();
            _createRocketTowerButton.onClick.RemoveAllListeners();
            _sellTowerButton.onClick.RemoveAllListeners();
        }
    }
}