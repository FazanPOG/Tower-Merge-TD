using System;
using System.Collections.Generic;
using System.Linq;
using Game.State;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.Gameplay
{
    public class DragAndDrop : MonoBehaviour, IDraggable
    {
        private const float DROP_ON_TOWER_DETECTION_RADIUS = 0.5f; 
        
        private Transform _draggedTransform;
        private Map _baseMap;
        private Map _environmentMap;
        private Tile[] _towerPlaces;
        private Tile[] _environment;

        private Vector2 _previousPosition;
        private Collider2D[] _colliders;
        private BoxCollider2D _dragCollider;
        private bool _isDragRequiredCollider;
        
        public event Action OnDroppedOnTileMap;
        public event Action<TowerObject> OnDroppedOnTower;
        
        public void Init(Transform draggedTransform, Map baseMap, Map environmentMap, Tile[] towerPlaces, Tile[] environment)
        {
            _draggedTransform = draggedTransform;
            _baseMap = baseMap;
            _environmentMap = environmentMap;
            _towerPlaces = towerPlaces;
            _environment = environment;

            _colliders = GetComponents<Collider2D>();
            _dragCollider = GetComponent<BoxCollider2D>();
        }
        
        public void StartDrag(Collider2D draggedObject)
        {
            _isDragRequiredCollider = _dragCollider == draggedObject;

            _previousPosition = _draggedTransform.position;
        }

        public void DragPerformed(Vector3 dragWorldPosition)
        {
            if(_isDragRequiredCollider == false) return;
            
            Vector3 target = new Vector3(dragWorldPosition.x, dragWorldPosition.y, 0f);
            _draggedTransform.position = target;
        }

        public void Drop(Vector3 dropWorldPosition)
        {
            if(_isDragRequiredCollider == false) return;
            
            bool droppedOnTower = HandleDropOnTower(dropWorldPosition);
            
            if (droppedOnTower == false)
                HandleDropOnTileMap(dropWorldPosition);
        }

        public void ResetPosition()
        {
            _draggedTransform.position = _previousPosition;
        }

        private bool HandleDropOnTower(Vector3 dropWorldPosition)
        {
            List<TowerObject> checkedTowers = new List<TowerObject>();
            var colliders = GetDroppedColliders(dropWorldPosition);

            foreach (var collider in colliders)
            {
                if(_colliders.Contains(collider)) continue;

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
            TileBase baseMapTile = _baseMap.GetTile(dropWorldPosition);
            TileBase environmentMapTile = _environmentMap.GetTile(dropWorldPosition);
            
            if (CanDrop(baseMapTile, environmentMapTile))
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
            var cellCenter = _baseMap.GetCellCenterPosition(dropWorldPosition);
            _draggedTransform.position = cellCenter;
        }
        
        private Collider2D[] GetDroppedColliders(Vector3 droppedPosition)
        {
            droppedPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapPointAll(droppedPosition);
            return colliders;
        }
        
        private bool CanDrop(TileBase baseMapTile, TileBase environmentMapTile)
        {
            return _towerPlaces.Contains(baseMapTile) && _environment.Contains(environmentMapTile) == false;
        }
    }
}