using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TutorialView : MonoBehaviour
    {
        private const string CLICK_ANIMATION_TRIGGER_KEY = "Click";
        private const string DRAG_ANIMATION_TRIGGER_KEY = "Drag";
        
        [SerializeField] private GameObject _tutorialTextView;
        [SerializeField] private TextMeshProUGUI _tutorialText;
        [SerializeField] private GameObject _hand;
        [SerializeField] private Animator _animator;

        private Camera _mainCamera;
        private Canvas _handParentCanvas;
        private RectTransform _handRectTransform;

        public void Init()
        {
            _mainCamera = Camera.main;
            _handParentCanvas = _hand.transform.GetComponentInParent<Canvas>();
            _handRectTransform = _hand.GetComponent<RectTransform>();
        }

        public void PlayClickAnimation()
        {
            _animator.SetTrigger(CLICK_ANIMATION_TRIGGER_KEY);
        }
        
        public void PlayDragAnimation()
        {
            _animator.SetTrigger(DRAG_ANIMATION_TRIGGER_KEY);
        }

        public void StopAnimation()
        {
            _animator.ResetTrigger(CLICK_ANIMATION_TRIGGER_KEY);
            _animator.ResetTrigger(DRAG_ANIMATION_TRIGGER_KEY);
        }
        
        public void SetActiveTutorialTextView(bool activeState) => _tutorialTextView.gameObject.SetActive(activeState);
        
        public void SetTutorialText(string text) => _tutorialText.text = text;

        public void SetActiveHandImage(bool activeState) => _hand.gameObject.SetActive(activeState);
        
        public void UpdateHandImagePosition(Vector2 worldPosition)
        {
            Vector2 canvasPosition = WorldToCanvasPosition(worldPosition);
            _handRectTransform.anchoredPosition = canvasPosition;
        }

        private Vector2 WorldToCanvasPosition(Vector2 worldPosition)
        {
            Vector2 screenPoint = _mainCamera.WorldToScreenPoint(worldPosition);
        
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _handParentCanvas.transform as RectTransform,
                screenPoint,
                _handParentCanvas.worldCamera,
                out Vector2 canvasPosition);

            return canvasPosition;
        }
    }
}