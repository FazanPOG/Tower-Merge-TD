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

        private Vector3 _targetPosition;
        private Vector2 _initialMousePosition;
        private float _dragDistance;
        private bool _isMousePressed;
        private bool _enabled;

        public event Action OnClicked;
        public event Action OnClickStarted;
        public event Action OnDragStarted;
        public event Action<Vector2> OnDragWithThreshold;
        public event Action OnClickCanceled;
        public event Action<float> OnZoomIn;
        public event Action<float> OnZoomOut;

        public DesktopInput(Camera camera)
        {
            _camera = camera;
            _playerInputActions = new PlayerInputActions();
            
            _playerInputActions.Enable();
            _playerInputActions.Mouse.Enable();
            _enabled = true;

            _playerInputActions.Mouse.LeftButton.started += LeftButtonStarted;
            _playerInputActions.Mouse.Delta.started += Delta;
            _playerInputActions.Mouse.Delta.performed += Delta;
            _playerInputActions.Mouse.LeftButton.canceled += LeftButtonCanceled;
            _playerInputActions.Mouse.Scroll.performed += ScrollPerformed;
        }

        public Vector3 GetInputWorldPosition()
        {
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
            return mouseWorldPosition;
        }

        public void EnableInput() => _enabled = true;
        public void DisableInput() => _enabled = false;

        private void LeftButtonStarted(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            if (_enabled == false) return;
            
            _isMousePressed = true;
            _initialMousePosition = GetInputWorldPosition();
            OnClickStarted?.Invoke();
        }

        private void Delta(InputAction.CallbackContext context)
        {
            if(_camera == null) return;
            if (_enabled == false) return;

            if (context.started)
            {
                OnDragStarted?.Invoke();
                return;
            }
            
            Vector2 currentMousePosition = GetInputWorldPosition();
            var distance = Vector2.Distance(_initialMousePosition, currentMousePosition);

            if (distance > MOUSE_DRAG_THRESHOLD && _isMousePressed)
            {
                _dragDistance = distance;
                OnDragWithThreshold?.Invoke(context.ReadValue<Vector2>());
            }
        }

        private void LeftButtonCanceled(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            if (_enabled == false) return;
            
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
                OnZoomIn?.Invoke(scrollValue.y);
            }
            else if (scrollValue.y < 0)
            {
                OnZoomOut?.Invoke(scrollValue.y);
            }
        }

        public void Dispose()
        {
            _playerInputActions.Mouse.Delta.performed -= Delta;
            _playerInputActions.Mouse.LeftButton.started -= LeftButtonStarted;
            _playerInputActions.Mouse.LeftButton.canceled -= LeftButtonCanceled;
            _playerInputActions.Mouse.Scroll.performed -= ScrollPerformed;
            _playerInputActions.Disable();
            _playerInputActions.Mouse.Disable();
        }
    }
}
