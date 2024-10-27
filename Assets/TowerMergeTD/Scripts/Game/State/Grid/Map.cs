using TowerMergeTD.Game.State;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.State
{
    public class Map
    {
        private readonly Tilemap _baseTileMap;
        private readonly TileSetConfig _tileSetConfig;

        public Map(Tilemap baseTileMap)
        {
            _baseTileMap = baseTileMap;
        }
        
        public Vector2Int GetCellPosition(Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3Int tilePos = _baseTileMap.WorldToCell(worldPos);
            return new Vector2Int(tilePos.x, tilePos.y);
        }

        public Vector2 GetCellCenterPosition(Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3Int tilePos = _baseTileMap.WorldToCell(worldPos);
            var cellCenter =  _baseTileMap.GetCellCenterWorld(tilePos);
            return new Vector2(cellCenter.x, cellCenter.y);
        }

        public TileBase GetTile(Vector3 position)
        {
            Vector3 worldPos = new Vector3(position.x, position.y, 0f);
            Vector3Int tilePos = _baseTileMap.WorldToCell(worldPos);
            return _baseTileMap.GetTile(tilePos);
        }
    }
}
