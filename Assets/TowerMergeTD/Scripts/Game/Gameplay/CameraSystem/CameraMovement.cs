using System;
using TowerMergeTD.Game.State;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.Gameplay
{
    public class CameraMovement : MonoBehaviour, IPauseHandler
    {
        private const float MOVE_SPEED = 10f;
        private const float MAX_CAMERA_SIZE = 5f;
        private const float MIN_CAMERA_SIZE = 3f;

        private Camera _camera;
        private Tilemap _tilemap;
        private IInput _input;
        private MapCoordinator _mapCoordinator;
        
        private Vector2 _targetPosition;    
        private bool _isDragging; 
        
        private float _zoom;
        private float _zoomMultiplier = 4f;
        private float _velocity;
        private float _smoothTime = 0.05f;

        private float _targetZoom;
        private Vector3 _minBounds;
        private Vector3 _maxBounds;
        private bool _isEnabled;

        public void Init(Camera mainCamera, Tilemap baseTileMap, IInput input, MapCoordinator mapCoordinator)
        {
            _camera = mainCamera;
            _tilemap = baseTileMap;
            _input = input;
            _mapCoordinator = mapCoordinator;
            
            CalculateTilemapBounds();
            _isEnabled = true;

            _input.OnDragStarted += StartDrag;
            _input.OnDragWithThreshold += UpdateDrag;
            _input.OnZoomIn += HandleZoom;
            _input.OnZoomOut += HandleZoom;
        }

        public void HandlePause(bool isPaused)
        {
            _isEnabled = !isPaused;
        }

        private void HandleZoom(float scroll)
        {
            if(_isEnabled == false)
                return;
            
            _zoom -= scroll * _zoomMultiplier;
            _zoom = Mathf.Clamp(_zoom, MIN_CAMERA_SIZE, MAX_CAMERA_SIZE);
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _zoom, ref _velocity, _smoothTime);
        }


        private void LateUpdate()
        {
            if (_isDragging)
            {
                transform.position = Vector2.Lerp(transform.position, _targetPosition, MOVE_SPEED * Time.unscaledDeltaTime);
                ClampCameraPosition();
            }
        }

        private void StartDrag()
        {
            _isDragging = !_mapCoordinator.HasTowerInCell(_input.GetInputWorldPosition(), out TowerObject _);
        }
        
        private void UpdateDrag(Vector2 input)
        {
            if (_isDragging)
            {
                float speed = 150f;
                Vector2 direction = input.normalized;
                Vector2 offset = -direction * speed * Time.unscaledDeltaTime;

                var target = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
                _targetPosition = target;
            }
        }
        

        private void ClampCameraPosition()
        {
            float halfHeight = _camera.orthographicSize;
            float halfWidth = halfHeight * _camera.aspect;
            
            float clampedX = Mathf.Clamp(transform.position.x, _minBounds.x + halfWidth, _maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, _minBounds.y + halfHeight, _maxBounds.y - halfHeight);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }

        private void CalculateTilemapBounds()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            Vector3Int min = bounds.max;
            Vector3Int max = bounds.min;

            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                if (_tilemap.HasTile(pos))
                {
                    min = Vector3Int.Min(min, pos);
                    max = Vector3Int.Max(max, pos);
                }
            }

            _minBounds = _tilemap.CellToWorld(min);
            _maxBounds = _tilemap.CellToWorld(max) + _tilemap.cellSize;
        }
    }
}