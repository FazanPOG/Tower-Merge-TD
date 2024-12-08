using System;
using System.Collections.Generic;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowersListView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TowerSpritesConfig _towerSpritesConfig;
        [SerializeField] private List<TowerCreateButtonView> _towerButtons;

        private Camera _mainCamera;
        private Canvas _parentCanvas;
        private RectTransform _rectTransform;

        public TowerSpritesConfig TowerSpritesConfig => _towerSpritesConfig;

        public List<TowerCreateButtonView> TowerButtons => _towerButtons;
        
        public bool CanDisable { get; set; }
        public bool IsMouseOver { get; private set; }
        
        private void Start()
        {
            _mainCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            _parentCanvas = GetComponentInParent<Canvas>();
            CanDisable = true;
            
            Hide();
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
    }
}