using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.State
{
    public class TilemapCoordinator
    {
        private readonly Tilemap _baseTilemap;
        private readonly Tilemap _environmentTilemap;
        private readonly TileSetConfig _tileSetConfig;

        public TilemapCoordinator(Tilemap baseTilemap, Tilemap environmentTilemap, TileSetConfig tileSetConfig)
        {
            _baseTilemap = baseTilemap;
            _environmentTilemap = environmentTilemap;
            _tileSetConfig = tileSetConfig;
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
            Vector3Int tilePos = Vector3Int.zero;
            
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

        public TileBase GetTile(TilemapType tilemapType, Vector3 position)
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