using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class GridTesting : MonoBehaviour
    {
        private const int GRID_WIDTH = 18;
        private const int GRID_HEIGHT = 10;
        private const int CELL_SIZE = 1;
        private readonly Vector2Int GRID_ORIGIN_POSITION = new Vector2Int(-9, -5);
        
        [SerializeField] private bool _ovverideDefaultGridSettings;
        
        [ShowIf(nameof(_ovverideDefaultGridSettings))]
        [SerializeField] private int _gridWidth;
        [ShowIf(nameof(_ovverideDefaultGridSettings))]
        [SerializeField] private int _gridHeight;
        [ShowIf(nameof(_ovverideDefaultGridSettings))]
        [SerializeField] private int _cellSize;
        [ShowIf(nameof(_ovverideDefaultGridSettings))]
        [SerializeField] private int2 _gridOriginPosition;

        private Grid _grid;
        
        private void Start()
        {
            if(_ovverideDefaultGridSettings)
                _grid = new Grid(_gridWidth, _gridHeight, _cellSize, new Vector2Int(_gridOriginPosition.x, _gridOriginPosition.y));
            else
                _grid = new Grid(GRID_WIDTH, GRID_HEIGHT, CELL_SIZE, GRID_ORIGIN_POSITION);
        }
    }
}
