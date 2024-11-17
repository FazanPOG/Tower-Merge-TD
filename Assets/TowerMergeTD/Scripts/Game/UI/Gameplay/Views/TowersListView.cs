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
        [SerializeField] private Sprite _lockSprite;

        [Header("Gun tower")]
        [SerializeField] private Button _gunTowerButton;
        [SerializeField] private Image _gunTowerImage;
        [SerializeField] private Sprite _gunTowerSprite;
        [Space(5)]
        
        [Header("Rocket tower")]
        [SerializeField] private Button _rocketTowerButton;
        [SerializeField] private Image _rocketTowerImage;
        [SerializeField] private Sprite _rocketTowerSprite;
        [Space(5)]
        
        [Header("Laser tower")]
        [SerializeField] private Button _laserTowerButton;
        [SerializeField] private Image _laserTowerImage;
        [SerializeField] private Sprite _laserTowerSprite;
        [Space(5)]
        
        [Header("Sniper tower")]
        [SerializeField] private Button _sniperTowerButton;
        [SerializeField] private Image _sniperTowerImage;
        [SerializeField] private Sprite _sniperTowerSprite;
        
        private Camera _mainCamera;
        private Canvas _parentCanvas;
        private RectTransform _rectTransform;

        private Dictionary<TowerType, Button> _towerTypeButtonMap;
        private Dictionary<TowerType, Image> _towerTypeImageMap;
        private Dictionary<TowerType, Sprite> _towerTypeSpriteMap;
        
        public bool CanDisable { get; set; }
        public bool IsMouseOver { get; private set; }
        
        public event Action<TowerType> OnCreateTowerButtonClicked;
        
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
            _gunTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Gun));
            _rocketTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Rocket));
            _laserTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Laser));
            _sniperTowerButton.onClick.AddListener(() => OnCreateTowerButtonClicked?.Invoke(TowerType.Sniper));
        }

        public void InitButtons()
        {
            _towerTypeButtonMap = new Dictionary<TowerType, Button>()
            {
                [TowerType.Gun] = _gunTowerButton,
                [TowerType.Rocket] = _rocketTowerButton,
                [TowerType.Laser] = _laserTowerButton,
                [TowerType.Sniper] = _sniperTowerButton,
            };
            
            _towerTypeImageMap = new Dictionary<TowerType, Image>()
            {
                [TowerType.Gun] = _gunTowerImage,
                [TowerType.Rocket] = _rocketTowerImage,
                [TowerType.Laser] = _laserTowerImage,
                [TowerType.Sniper] = _sniperTowerImage,
            };
            
            _towerTypeSpriteMap = new Dictionary<TowerType, Sprite>()
            {
                [TowerType.Gun] = _gunTowerSprite,
                [TowerType.Rocket] = _rocketTowerSprite,
                [TowerType.Laser] = _laserTowerSprite,
                [TowerType.Sniper] = _sniperTowerSprite,
            };
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

        public bool HasCreateButton(TowerType type)
        {
            return _towerTypeButtonMap.ContainsKey(type);
        }

        public void LockAllButtons()
        {
            foreach (var towerType in _towerTypeButtonMap.Keys)
                SetTowerCreateButtonActiveState(towerType, false);
        }

        public void UnlockAllButtons()
        {
            foreach (var towerType in _towerTypeButtonMap.Keys)
                SetTowerCreateButtonActiveState(towerType, true);
        }
        
        public void SetTowerCreateButtonActiveState(TowerType towerType, bool activeState)
        {
            Button button = _towerTypeButtonMap[towerType];
            Image image = _towerTypeImageMap[towerType];
            
            if (activeState)
            {
                Sprite sprite = _towerTypeSpriteMap[towerType];

                button.interactable = true;
                image.sprite = sprite;
            }
            else
            {
                image.sprite = _lockSprite;
                button.interactable = false;
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
            _laserTowerButton.onClick.RemoveAllListeners();
            _sniperTowerButton.onClick.RemoveAllListeners();
        }
    }
}