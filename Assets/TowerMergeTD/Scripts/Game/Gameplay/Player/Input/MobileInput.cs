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
        private Vector2 _secondaryTouchStartPos;
        private bool _isSecondaryTouchActive = false;
        private float _previousDistance = 0f;
        
        private Vector2 _initialTouchPosition;
        private bool _isTouchScreenPressed;
        private float _dragDistance;

        public event Action OnClicked;
        public event Action OnClickStarted;
        public event Action OnDrag;
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
            _playerInputActions.TouchScreen.SecondaryTouchPressed.canceled += HandleSecondaryTouchPressed;
            
            _playerInputActions.TouchScreen.SecondaryTouchDelta.performed += SecondaryTouchDeltaPerformed;
        }

        private void HandlePrimaryTouchPressed(InputAction.CallbackContext context)
        {
            if(_camera == null) return;

            if (context.started)
            {
                Debug.Log("PrimaryTouchPressed START");
                _primaryTouchStartPos = Touchscreen.current.primaryTouch.position.ReadValue();
                _isTouchScreenPressed = true;
                _initialTouchPosition = GetClickWorldPosition();
                OnClickStarted?.Invoke();
            }

            if (context.canceled)
            { 
                if(_camera == null) return;
            
                Debug.Log("PrimaryTouchPressed CANCELED");
                
                if (_isTouchScreenPressed && _dragDistance < TOUCH_DRAG_THRESHOLD)
                    OnClicked?.Invoke();

                _dragDistance = 0f;
                _isTouchScreenPressed = false;
                
                OnClickCanceled?.Invoke();
            }
        }

        private void PrimaryTouchDeltaPerformed(InputAction.CallbackContext _)
        {
            if(_camera == null) return;
            
            if (!_isSecondaryTouchActive)
                return;

            Vector2 primaryTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 secondaryTouchPos = Touchscreen.current.touches[1].position.ReadValue();

            float currentDistance = Vector2.Distance(primaryTouchPos, secondaryTouchPos);
            float deltaMagnitude = currentDistance - _previousDistance;
            float zoomSpeed = Mathf.Abs(deltaMagnitude) * Time.deltaTime;

            if (deltaMagnitude > 0)
            {
                OnZoomIn?.Invoke(zoomSpeed);
            }
            else if (deltaMagnitude < 0)
            {
                OnZoomOut?.Invoke(zoomSpeed);
            }

            _previousDistance = currentDistance;
            
            Debug.Log("PrimaryTouchPositionPerformed");
            
            Vector2 currentMousePosition = GetClickWorldPosition();
            var distance = Vector2.Distance(_initialTouchPosition, currentMousePosition);
            
            if (distance > TOUCH_DRAG_THRESHOLD && _isTouchScreenPressed)
            {
                _dragDistance = distance;
                OnDrag?.Invoke();
            }
        }

        private void HandleSecondaryTouchPressed(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _secondaryTouchStartPos = Touchscreen.current.touches[1].position.ReadValue();
                _isSecondaryTouchActive = true;

                _previousDistance = Vector2.Distance(_primaryTouchStartPos, _secondaryTouchStartPos);
                Debug.Log("Secondary touch START");
            }

            if (context.canceled)
            {
                Debug.Log("Secondary touch CANCEL");
            }
        }

        private void SecondaryTouchDeltaPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("SecondaryTouchDelta Performed");
            PrimaryTouchDeltaPerformed(context);
        }

        private void ResetTouch()
        {
            _isSecondaryTouchActive = false;
            _previousDistance = 0f;
        }
        
        public Vector3 GetClickWorldPosition()
        {
            return Vector3.zero;
        }

        public void Dispose()
        {
            _playerInputActions.TouchScreen.PrimaryTouchPressed.started -= HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchPressed.canceled -= HandlePrimaryTouchPressed;
            _playerInputActions.TouchScreen.PrimaryTouchDelta.performed -= PrimaryTouchDeltaPerformed;
            _playerInputActions.TouchScreen.SecondaryTouchPressed.started -= HandleSecondaryTouchPressed;
            _playerInputActions.TouchScreen.SecondaryTouchPressed.canceled -= HandleSecondaryTouchPressed;
            _playerInputActions.TouchScreen.SecondaryTouchDelta.performed -= SecondaryTouchDeltaPerformed;
            _playerInputActions.TouchScreen.Disable();
            _playerInputActions.Disable();
            _playerInputActions?.Dispose();
        }
    }
}