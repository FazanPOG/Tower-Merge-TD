using System;
using System.Collections.Generic;
using R3;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class DragAndDrop : MonoBehaviour, IPauseHandler
    {
        private const float DROP_ON_TOWER_DETECTION_RADIUS = 0.5f; 
        
        private IInput _input;
        private Transform _draggedTransform;
        private MapCoordinator _mapCoordinator;

        private Vector2 _previousPosition;
        private BoxCollider2D _dragCollider;
        private bool _canDrag;
        private ReactiveProperty<bool> _isDragging = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsDragging => _isDragging;
        
        public event Action OnDroppedOnTileMap;
        public event Action<TowerObject> OnDroppedOnTower;

        public void Init(IInput input, Transform draggedTransform, MapCoordinator mapCoordinator)
        {
            _input = input;
            _draggedTransform = draggedTransform;
            _mapCoordinator = mapCoordinator;
            
            _dragCollider = GetComponent<BoxCollider2D>();
            _canDrag = true;
            
            _input.OnClickStarted += TryDrag;
            _input.OnDragWithThreshold += DragPerformed;
            _input.OnClickCanceled += Drop;
        }

        public void ResetPosition() => _draggedTransform.position = _previousPosition;

        public void HandlePause(bool isPaused) => _canDrag = !isPaused;

        private void TryDrag()
        {
            if(_canDrag == false) return;
            
            bool isClickedDraggableCollider = IsClickedDraggableCollider();

            if (isClickedDraggableCollider)
            {
                _isDragging.Value = true;
                _previousPosition = _draggedTransform.position;
            }
        }

        private void DragPerformed(Vector2 _)
        {
            if(_canDrag == false) return;
            if(_isDragging.CurrentValue == false) return;

            var dragWorldPosition = _input.GetInputWorldPosition();
            Vector3 target = new Vector3(dragWorldPosition.x, dragWorldPosition.y, 0f);
            _draggedTransform.position = target;
        }

        private void Drop()
        {
            if(_canDrag == false) return;
            if(_isDragging.CurrentValue == false) return;
            
            var dropWorldPosition = _input.GetInputWorldPosition();
            bool droppedOnTower = HandleDropOnTower(dropWorldPosition);

            if (droppedOnTower == false)
                HandleDropOnTileMap(dropWorldPosition);
            
            _isDragging.Value = false;
        }

        private bool IsClickedDraggableCollider()
        {
            Vector3 mouseWorldPosition = _input.GetInputWorldPosition();
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
            _input.OnClickStarted -= TryDrag;
            _input.OnDragWithThreshold -= DragPerformed;
            _input.OnClickCanceled -= Drop;
        }
    }
}