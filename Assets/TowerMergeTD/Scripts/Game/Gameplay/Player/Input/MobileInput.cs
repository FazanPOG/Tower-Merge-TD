using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerMergeTD.Game.Gameplay
{
    public class MobileInput : IInput, IDisposable
    {
        private const float TOUCH_DRAG_THRESHOLD = .25f;
        
        private readonly Camera _camera;
        private readonly PlayerInputActions _playerInputActions;

        private Vector2 _primaryTouchStartPos;
        private Vector2 _initialTouchPosition;
        private bool _isSecondaryTouchActive;
        private bool _isTouchScreenPressed;
        private float _previousDistance;
        private float _dragDistance;

        public event Action OnClicked;
        public event Action OnClickStarted;
        public event Action OnDragStarted;
        public event Action<Vector2> OnDragWithThreshold;
        public event Action OnClickCanceled;
        public event Action<float> OnZoomIn;
        public event Action<float> OnZoomOut;

        public MobileInput(Camera camera)
        {
            _camera = camera;
            
            _playerInputActions = new PlayerInputActions();
            
            _playerInputActions.Enable();
            _playerInputActions.TouchScreen.Enable();
            
            _playerInputActions.TouchScreen.PrimaryTouchPressed.started += HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchPressed.canceled += HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchDelta.performed += PrimaryTouchDeltaPerformed;
            
            _playerInputActions.TouchScreen.SecondaryTouchPressed.started += HandleSecondaryTouchPressed;
            _playerInputActions.TouchScreen.SecondaryTouchPressed.canceled += HandleSecondaryTouchCanceled;
            _playerInputActions.TouchScreen.SecondaryTouchDelta.performed += SecondaryTouchDeltaPerformed;
        }

        private void HandlePrimaryTouchPressed(InputAction.CallbackContext context)
        {
            if (_camera == null) return;

            if (context.started)
            {
                _primaryTouchStartPos = Touchscreen.current.primaryTouch.position.ReadValue();
                _isTouchScreenPressed = true;
                _initialTouchPosition = GetInputWorldPosition();
                OnClickStarted?.Invoke();
            }

            if (context.canceled)
            {
                if (_isTouchScreenPressed && _dragDistance < TOUCH_DRAG_THRESHOLD)
                    OnClicked?.Invoke();

                _dragDistance = 0f;
                _isTouchScreenPressed = false;
                OnClickCanceled?.Invoke();
            }
        }

        private void PrimaryTouchDeltaPerformed(InputAction.CallbackContext _)
        {
            if (_camera == null || _isSecondaryTouchActive) return;

            Vector2 currentTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            var distance = Vector2.Distance(_initialTouchPosition, currentTouchPosition);

            if (distance > TOUCH_DRAG_THRESHOLD && _isTouchScreenPressed)
            {
                _dragDistance = distance;
                OnDragWithThreshold?.Invoke(currentTouchPosition - _primaryTouchStartPos);
                OnDragStarted?.Invoke();
            }
        }

        private void HandleSecondaryTouchPressed(InputAction.CallbackContext context)
        {
            if (context.started && Touchscreen.current.touches.Count > 1)
            {
                _isSecondaryTouchActive = true;
                _previousDistance = Vector2.Distance(
                    Touchscreen.current.primaryTouch.position.ReadValue(),
                    Touchscreen.current.touches[1].position.ReadValue()
                );
            }
        }

        private void HandleSecondaryTouchCanceled(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _isSecondaryTouchActive = false;
                _previousDistance = 0f;
            }
        }

        private void SecondaryTouchDeltaPerformed(InputAction.CallbackContext context)
        {
            if (!_isSecondaryTouchActive || Touchscreen.current.touches.Count < 2) return;

            Vector2 primaryTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 secondaryTouchPos = Touchscreen.current.touches[1].position.ReadValue();

            float currentDistance = Vector2.Distance(primaryTouchPos, secondaryTouchPos);
            float deltaMagnitude = currentDistance - _previousDistance;

            if (Mathf.Abs(deltaMagnitude) > 0.01f)
            {
                if (deltaMagnitude > 0)
                {
                    OnZoomIn?.Invoke(deltaMagnitude);
                }
                else if (deltaMagnitude < 0)
                {
                    OnZoomOut?.Invoke(-deltaMagnitude);
                }
            }

            _previousDistance = currentDistance;
        }
        
        public Vector3 GetInputWorldPosition()
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 touchScreenPosition = new Vector3(touchPosition.x, touchPosition.y, _camera.nearClipPlane);
            return _camera.ScreenToWorldPoint(touchScreenPosition);
        }

        public void Dispose()
        {
            _playerInputActions.TouchScreen.PrimaryTouchPressed.started -= HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchPressed.canceled -= HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchDelta.performed -= PrimaryTouchDeltaPerformed;
            _playerInputActions.TouchScreen.SecondaryTouchPressed.started -= HandleSecondaryTouchPressed;
            _playerInputActions.TouchScreen.SecondaryTouchPressed.canceled -= HandleSecondaryTouchCanceled;
            _playerInputActions.TouchScreen.SecondaryTouchDelta.performed -= SecondaryTouchDeltaPerformed;
            _playerInputActions.TouchScreen.Disable();
            _playerInputActions.Disable();
            _playerInputActions.Dispose();
        }
    }
}