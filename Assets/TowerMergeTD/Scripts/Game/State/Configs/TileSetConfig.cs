using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.State
{
    [CreateAssetMenu(menuName = "Configs/Map/TileSetConfig")]
    public class TileSetConfig : ScriptableObject
    {
        [ListDrawerSettings(ShowIndexLabels = true)]
        [InlineProperty, PreviewField(50, ObjectFieldAlignment.Left)] 
        [SerializeField] private Tile[] _environmentTiles;
        
        [ListDrawerSettings(ShowIndexLabels = true)]
        [InlineProperty, PreviewField(50, ObjectFieldAlignment.Left)]
        [SerializeField] private Tile[] _towerPlaces;

        public Tile[] EnvironmentTiles => _environmentTiles;
        public Tile[] TowerPlaces => _towerPlaces;
    }
}