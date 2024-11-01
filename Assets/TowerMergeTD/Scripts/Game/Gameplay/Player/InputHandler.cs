using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerMergeTD.Game.Gameplay
{
    public class InputHandler : IDisposable
    {
        private const float MOUSE_DRAG_THRESHOLD = .25f;
        
        private Camera _camera;

        private PlayerInputActions _playerInputActions;
        private Vector2 _initialMousePosition;
        private float _dragDistance;
        private bool _isMousePressed;

        public event Action OnMouseClicked;
        public event Action OnMouseClickStarted;
        public event Action OnMouseDrag;
        public event Action OnMouseCanceled;
        
        public InputHandler()
        {
            _camera = Camera.main;
            _playerInputActions = new PlayerInputActions();
            
            _playerInputActions.Enable();
            _playerInputActions.Mouse.Enable();
            
            _playerInputActions.Mouse.Delta.performed += DeltaPerformed;
            _playerInputActions.Mouse.LeftButton.started += LeftButtonStarted;
            _playerInputActions.Mouse.LeftButton.canceled += LeftButtonCanceled;
        }

        public Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
            return mouseWorldPosition;
        }

        private void LeftButtonStarted(InputAction.CallbackContext _)
        {
            _isMousePressed = true;
            _initialMousePosition = GetMouseWorldPosition();
            OnMouseClickStarted?.Invoke();
        }

        private void DeltaPerformed(InputAction.CallbackContext _)
        {
            Vector2 currentMousePosition = GetMouseWorldPosition();
            var distance = Vector2.Distance(_initialMousePosition, currentMousePosition);
            
            if (distance > MOUSE_DRAG_THRESHOLD && _isMousePressed)
            {
                _dragDistance = distance;
                OnMouseDrag?.Invoke();
            }
        }

        private void LeftButtonCanceled(InputAction.CallbackContext _)
        {
            if (_isMousePressed && _dragDistance < MOUSE_DRAG_THRESHOLD)
                OnMouseClicked?.Invoke();

            _dragDistance = 0f;
            _isMousePressed = false;
            OnMouseCanceled?.Invoke();
        }

        public void Dispose()
        {
            _playerInputActions.Mouse.Delta.performed -= DeltaPerformed;
            _playerInputActions.Mouse.LeftButton.started -= LeftButtonStarted;
            _playerInputActions.Mouse.LeftButton.canceled -= LeftButtonCanceled;
            _playerInputActions?.Dispose();
        }
    }
}
