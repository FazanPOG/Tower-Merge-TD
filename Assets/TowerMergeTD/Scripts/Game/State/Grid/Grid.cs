using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class Grid
    {
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector2Int _originPosition;

        private int[,] _gridArray;
        
        public Grid(int width, int height, float cellSize, Vector2Int originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
            CreateGrid();
        }

        private void CreateGrid()
        {
            _gridArray = new int[_width, _height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    //WorldTextCreator.CreateWorldText(_gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, 5);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + (int)_cellSize), Color.red, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + (int)_cellSize, y), Color.red, 100f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.red, 100f);
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + new Vector3(_originPosition.x, _originPosition.y, 0);
        }
    }
}
