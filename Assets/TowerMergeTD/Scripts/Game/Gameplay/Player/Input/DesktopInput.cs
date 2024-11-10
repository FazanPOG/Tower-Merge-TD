using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerMergeTD.Game.Gameplay
{
    public class DesktopInput : IInput, IDisposable
    {
        private const float MOUSE_DRAG_THRESHOLD = .25f;

        private readonly Camera _camera;
        private readonly PlayerInputActions _playerInputActions;

        private Vector2 _initialMousePosition;
        private float _dragDistance;
        private bool _isMousePressed;

        public event Action OnClicked;
        public event Action OnClickStarted;
        public event Action OnDrag;
        public event Action OnClickCanceled;
        public event Action<float> OnZoomIn;
        public event Action<float> OnZoomOut;

        public DesktopInput(Camera camera)
        {
            _camera = camera;
            _playerInputActions = new PlayerInputActions();
            
            _playerInputActions.Enable();
            _playerInputActions.Mouse.Enable();

            _playerInputActions.Mouse.LeftButton.started += LeftButtonStarted;
            _playerInputActions.Mouse.Delta.performed += DeltaPerformed;
            _playerInputActions.Mouse.LeftButton.canceled += LeftButtonCanceled;
            _playerInputActions.Mouse.Scroll.performed += ScrollPerformed;
        }

        public Vector3 GetClickWorldPosition()
        {
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
            return mouseWorldPosition;
        }

        private void LeftButtonStarted(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            
            _isMousePressed = true;
            _initialMousePosition = GetClickWorldPosition();
            OnClickStarted?.Invoke();
        }

        private void DeltaPerformed(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            
            Vector2 currentMousePosition = GetClickWorldPosition();
            var distance = Vector2.Distance(_initialMousePosition, currentMousePosition);
            
            if (distance > MOUSE_DRAG_THRESHOLD && _isMousePressed)
            {
                _dragDistance = distance;
                OnDrag?.Invoke();
            }
        }

        private void LeftButtonCanceled(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            
            if (_isMousePressed && _dragDistance < MOUSE_DRAG_THRESHOLD)
                OnClicked?.Invoke();

            _dragDistance = 0f;
            _isMousePressed = false;
            OnClickCanceled?.Invoke();
        }

        private void ScrollPerformed(InputAction.CallbackContext context)
        {
            Vector2 scrollValue = context.ReadValue<Vector2>();

            if (scrollValue.y > 0)
            {
                Debug.Log("OnZoomIn");
                OnZoomIn?.Invoke(scrollValue.y);
            }
            else if (scrollValue.y < 0)
            {
                Debug.Log("OnZoomOut");
                OnZoomOut?.Invoke(scrollValue.y);
            }
        }

        public void Dispose()
        {
            _playerInputActions.Mouse.Delta.performed -= DeltaPerformed;
            _playerInputActions.Mouse.LeftButton.started -= LeftButtonStarted;
            _playerInputActions.Mouse.LeftButton.canceled -= LeftButtonCanceled;
            _playerInputActions.Mouse.Scroll.performed -= ScrollPerformed;
            _playerInputActions.Disable();
            _playerInputActions.Mouse.Disable();
        }
    }
}
