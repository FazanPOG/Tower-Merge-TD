using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tutorialText;
        [SerializeField] private Image _handImage;

        private Camera _mainCamera;
        private Canvas _handParentCanvas;

        public void Init()
        {
            _mainCamera = Camera.main;
            _handParentCanvas = _handImage.transform.GetComponentInParent<Canvas>();
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        
        public void SetTutorialText(string text) => _tutorialText.text = text;

        public void SetActiveHandImage(bool activeState) => _handImage.gameObject.SetActive(activeState);
        
        public void UpdateHandImagePosition(Vector2 worldPosition)
        {
            Vector2 canvasPosition = WorldToCanvasPosition(worldPosition);
            _handImage.rectTransform.anchoredPosition = canvasPosition;
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