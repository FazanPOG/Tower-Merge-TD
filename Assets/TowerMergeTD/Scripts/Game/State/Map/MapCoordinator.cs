using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.State
{
    public class MapCoordinator
    {
        private readonly Tilemap _baseTilemap;
        private readonly Tilemap _environmentTilemap;
        private readonly TileSetConfig _tileSetConfig;

        public MapCoordinator(Tilemap baseTilemap, Tilemap environmentTilemap, TileSetConfig tileSetConfig)
        {
            _baseTilemap = baseTilemap;
            _environmentTilemap = environmentTilemap;
            _tileSetConfig = tileSetConfig;
        }

        public List<Vector2> GetAllTowerPlaceTileWorldPositions()
        {
            List<Vector2> towerPlaceTiles = new List<Vector2>();

            BoundsInt bounds = _baseTilemap.cellBounds;
            foreach (var position in bounds.allPositionsWithin)
            {
                var tile = _baseTilemap.GetTile(position);
            
                if (tile != null && _tileSetConfig.TowerPlaces.Contains(tile))
                {
                    Vector2 worldPosition = _baseTilemap.CellToWorld(position);
                    towerPlaceTiles.Add(worldPosition);
                }
            }

            return towerPlaceTiles;
        }

        public Vector2 GetFirstTowerPlaceTileWorldPosition()
        {
            List<Vector2> towerPlaceTiles = GetAllTowerPlaceTileWorldPositions();
        
            if (towerPlaceTiles.Count > 0)
            {
                return towerPlaceTiles[0];
            }

            throw new MissingComponentException("Missing tower place tile");
        }
        
        public Vector2 GetTileWorldPosition(Vector2 mouseWorldPosition)
        {
            Vector3Int tilePosition = _baseTilemap.WorldToCell(mouseWorldPosition);
            Vector3 tileWorldPosition = _baseTilemap.CellToWorld(tilePosition);

            return tileWorldPosition;
        }
        
        public bool CanPlaceTower(Vector3 worldPosition)
        {
            TileBase baseMapTile = GetTile(TilemapType.Base, worldPosition);
            TileBase environmentMapTile = GetTile(TilemapType.Environment, worldPosition);
            
            return _tileSetConfig.TowerPlaces.Contains(baseMapTile) && _tileSetConfig.EnvironmentTiles.Contains(environmentMapTile) == false;
        }
        
        public Vector2Int GetCellPosition(TilemapType tilemapType, Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3Int tilePos;
            
            switch (tilemapType)
            {
                case TilemapType.Base:
                    tilePos = _baseTilemap.WorldToCell(worldPos);
                    break;
                
                case TilemapType.Environment:
                    tilePos = _environmentTilemap.WorldToCell(worldPos);
                    break;
                
                default:
                    throw new MissingComponentException($"Tilemap of type {tilemapType} is not supported or is missing.");
            }
            
            return new Vector2Int(tilePos.x, tilePos.y);
        }

        public Vector2 GetCellCenterPosition(TilemapType tilemapType, Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3 cellCenter = Vector3.zero;
            Vector3Int tilePos = Vector3Int.zero;
            
            switch (tilemapType)
            {
                case TilemapType.Base:
                    tilePos = _baseTilemap.WorldToCell(worldPos);
                    cellCenter =  _baseTilemap.GetCellCenterWorld(tilePos);
                    break;
                
                case TilemapType.Environment:
                    tilePos = _environmentTilemap.WorldToCell(worldPos);
                    cellCenter =  _environmentTilemap.GetCellCenterWorld(tilePos);
                    break;
                
                default:
                    throw new MissingComponentException($"Tilemap of type {tilemapType} is not supported or is missing.");
            }
            
            return new Vector2(cellCenter.x, cellCenter.y);
        }

        public bool HasTowerInCell(Vector3 worldPosition, out TowerObject towerObject)
        {
            worldPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapPointAll(worldPosition);
            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider is BoxCollider2D == false) 
                        continue;
                    
                    if (collider.gameObject.transform.parent.TryGetComponent(out TowerObject tower))
                    {
                        towerObject = tower;
                        return true;
                    }
                        
                }
            }

            towerObject = null;
            return false;
        }
        
        private TileBase GetTile(TilemapType tilemapType, Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3Int tilePos;
            
            switch (tilemapType)
            {
                case TilemapType.Base:
                    tilePos = _baseTilemap.WorldToCell(worldPos);
                    return _baseTilemap.GetTile(tilePos);
                
                case TilemapType.Environment:
                    tilePos = _environmentTilemap.WorldToCell(worldPos);
                    return _environmentTilemap.GetTile(tilePos);
                
                default:
                    throw new MissingComponentException($"Tilemap of type {tilemapType} is not supported or is missing.");
            }
        }
    }
}