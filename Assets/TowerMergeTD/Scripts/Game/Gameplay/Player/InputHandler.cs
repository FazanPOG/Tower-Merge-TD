using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerMergeTD.Game.Gameplay
{
    public class InputHandler : IDisposable
    {
        private Camera _camera;
        private IDraggable _currentDraggedObject;

        private PlayerInputActions _playerInputActions;
        
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

        private void LeftButtonStarted(InputAction.CallbackContext _)
        {
            TryDrag();
        }

        private void TryDrag()
        {
            Collider2D collider = GetClickedDraggableCollider();

            if(collider == null)
                return;

            collider.TryGetComponent(out IDraggable draggable);
            
            _currentDraggedObject = draggable;
            _currentDraggedObject?.StartDrag(collider);
        }
        
        private void DeltaPerformed(InputAction.CallbackContext _)
        {
            _currentDraggedObject?.DragPerformed(GetMouseWorldPosition());
        }

        private void LeftButtonCanceled(InputAction.CallbackContext _)
        {
            _currentDraggedObject?.Drop(GetMouseWorldPosition());
            _currentDraggedObject = null;
        }

        private Collider2D GetClickedDraggableCollider()
        {
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapPointAll(mouseWorldPosition);
            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider is BoxCollider2D)
                    {
                        return collider;
                    }
                }
                return colliders[0];
            }

            return null;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
            return mouseWorldPosition;
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
