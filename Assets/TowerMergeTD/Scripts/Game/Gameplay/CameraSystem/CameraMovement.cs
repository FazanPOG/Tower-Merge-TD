﻿using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.Gameplay
{
    public class CameraMovement : MonoBehaviour
    {
        private const string TILEMAP_BASE_NAME = "Tilemap-Base";
        private const float MOVE_SPEED = 10f;
        private const float MAX_CAMERA_SIZE = 5f;
        private const float MIN_CAMERA_SIZE = 3f;

        private Camera _camera;
        private Tilemap _tilemap;

        private Vector3 _minBounds;
        private Vector3 _maxBounds;
        private bool _isEnabled;

        public void Init(Camera mainCamera, Tilemap baseTileMap)
        {
            _camera = mainCamera;
            _tilemap = baseTileMap;
            
            Tilemap[] maps = FindObjectsOfType<Tilemap>();
            _tilemap = maps.First(x => x.name == TILEMAP_BASE_NAME);
            
            CalculateTilemapBounds();

            _isEnabled = true;
        }

        private void Update()
        {
            if(_isEnabled == false)
                return;
            
            MoveCamera();
            ClampCameraPosition();
        }

        private void MoveCamera()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, moveY, 0) * (MOVE_SPEED * Time.deltaTime);
            transform.position += move;
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