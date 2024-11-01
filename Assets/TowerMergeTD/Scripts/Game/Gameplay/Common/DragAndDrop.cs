using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class DragAndDrop : MonoBehaviour
    {
        private const float DROP_ON_TOWER_DETECTION_RADIUS = 0.5f; 
        
        private InputHandler _inputHandler;
        private Transform _draggedTransform;
        private MapCoordinator _mapCoordinator;

        private Vector2 _previousPosition;
        private BoxCollider2D _dragCollider;
        private bool _isDragging;
        
        public event Action OnDroppedOnTileMap;
        public event Action<TowerObject> OnDroppedOnTower;

        public void Init(InputHandler inputHandler, Transform draggedTransform, MapCoordinator mapCoordinator)
        {
            _inputHandler = inputHandler;
            _draggedTransform = draggedTransform;
            _mapCoordinator = mapCoordinator;

            _dragCollider = GetComponent<BoxCollider2D>();

            _inputHandler.OnMouseClickStarted += TryDrag;
            _inputHandler.OnMouseDrag += DragPerformed;
            _inputHandler.OnMouseCanceled += Drop;
        }

        public void ResetPosition() => _draggedTransform.position = _previousPosition;

        private void TryDrag()
        {
            bool isClickedDraggableCollider = IsClickedDraggableCollider();

            if (isClickedDraggableCollider)
            {
                _isDragging = true;
                _previousPosition = _draggedTransform.position;
            }
        }

        private void DragPerformed()
        {
            if(_isDragging == false) return;

            var dragWorldPosition = _inputHandler.GetMouseWorldPosition();
            Vector3 target = new Vector3(dragWorldPosition.x, dragWorldPosition.y, 0f);
            _draggedTransform.position = target;
        }

        private void Drop()
        {
            if(_isDragging == false) return;
            
            var dropWorldPosition = _inputHandler.GetMouseWorldPosition();
            bool droppedOnTower = HandleDropOnTower(dropWorldPosition);

            if (droppedOnTower == false)
                HandleDropOnTileMap(dropWorldPosition);
            
            _isDragging = false;
        }

        private bool IsClickedDraggableCollider()
        {
            Vector3 mouseWorldPosition = _inputHandler.GetMouseWorldPosition();
            mouseWorldPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapPointAll(mouseWorldPosition);
            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider == _dragCollider)
                        return true;
                }
            }

            return false;
        }

        private bool HandleDropOnTower(Vector3 dropWorldPosition)
        {
            List<TowerObject> checkedTowers = new List<TowerObject>();
            var colliders = GetDroppedColliders(dropWorldPosition);

            foreach (var collider in colliders)
            {
                if(collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()) continue;

                if (collider.gameObject.transform.parent.TryGetComponent(out TowerObject tower) &&
                    checkedTowers.Contains(tower) == false)
                {
                    float distance = Vector2.Distance(dropWorldPosition, tower.transform.position);
                    if (distance <= DROP_ON_TOWER_DETECTION_RADIUS)
                    {
                        UpdatePosition(dropWorldPosition);
                        OnDroppedOnTower?.Invoke(tower);
                        return true;
                    }
                    
                    checkedTowers.Add(tower);
                }
            }

            return false;
        }

        private void HandleDropOnTileMap(Vector3 dropWorldPosition)
        {
            if (_mapCoordinator.CanPlaceTower(dropWorldPosition))
            {
                UpdatePosition(dropWorldPosition);
                OnDroppedOnTileMap?.Invoke();
            }
            else
            {
                ResetPosition();
            }
        }

        private void UpdatePosition(Vector3 dropWorldPosition)
        {
            var cellCenter = _mapCoordinator.GetCellCenterPosition(TilemapType.Base, dropWorldPosition);
            _draggedTransform.position = cellCenter;
        }

        private Collider2D[] GetDroppedColliders(Vector3 droppedPosition)
        {
            droppedPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapPointAll(droppedPosition);
            return colliders;
        }

        private void OnDisable()
        {
            _inputHandler.OnMouseClickStarted -= TryDrag;
            _inputHandler.OnMouseDrag -= DragPerformed;
            _inputHandler.OnMouseCanceled -= Drop;
        }
    }
}