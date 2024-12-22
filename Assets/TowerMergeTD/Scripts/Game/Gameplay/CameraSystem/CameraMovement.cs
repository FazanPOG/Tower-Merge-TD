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

        [SerializeField] [Range(0.1f, 1f)] private float _visibleWidthPercentage = 1f;
        [SerializeField] private bool _disableUpdate;
        
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
            //_input.OnZoomIn += HandleZoom;
            //_input.OnZoomOut += HandleZoom;
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
            if(_disableUpdate)
                return;
            
            if(_isEnabled == false)
                return;
            
            if (_isDragging)
            {
                transform.position = Vector2.Lerp(transform.position, _targetPosition, MOVE_SPEED * Time.unscaledDeltaTime);
            }
            
            AdjustCameraSize();
            ClampCamera();
        }

        private void StartDrag()
        {
            if(_isEnabled == false)
                return;
            
            _isDragging = !_mapCoordinator.HasTowerInCell(_input.GetInputWorldPosition(), out TowerObject _);
        }

        private void UpdateDrag(Vector2 input)
        {
            if(_isEnabled == false)
                return;
            
            if (_isDragging)
            {
                float speed = 150f;
                Vector2 direction = input.normalized;
                Vector2 offset = -direction * speed * Time.unscaledDeltaTime;

                var target = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
                _targetPosition = target;
            }
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

        private void AdjustCameraSize()
        {
            float tilemapWidth = _maxBounds.x - _minBounds.x;
            float targetWidth = tilemapWidth * _visibleWidthPercentage;
            float screenAspect = (float)Screen.width / Screen.height;

            _camera.orthographicSize = targetWidth / (2f * screenAspect);

            // Проверка, чтобы размер камеры не был больше высоты уровня
            float maxCameraHeight = (_maxBounds.y - _minBounds.y) / 2f;
            _camera.orthographicSize = Mathf.Min(_camera.orthographicSize, maxCameraHeight);
        }

        private void ClampCamera()
        {
            float cameraHeight = _camera.orthographicSize;
            float cameraWidth = cameraHeight * _camera.aspect;

            float minX = _minBounds.x + cameraWidth;
            float maxX = _maxBounds.x - cameraWidth;
            float minY = _minBounds.y + cameraHeight;
            float maxY = _maxBounds.y - cameraHeight;

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
            clampedPosition.y = Mathf.Clamp(transform.position.y, minY, maxY);

            transform.position = clampedPosition;
        }
    }
}