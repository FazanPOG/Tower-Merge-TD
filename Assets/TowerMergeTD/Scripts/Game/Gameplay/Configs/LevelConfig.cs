using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _initialHealth;
        [SerializeField] private int _initialMoney;
        [SerializeField] private TileSetConfig _tileSetConfig;
        [SerializeField] private TowerGenerationConfig[] _towerGenerationConfigs;
        [SerializeField] private WaveConfig[] _waves;

        public int InitialHealth => _initialHealth;
        public int InitialMoney => _initialMoney;

        public TileSetConfig TileSetConfig => _tileSetConfig;
        
        public TowerGenerationConfig[] TowerGenerationConfigs => _towerGenerationConfigs;
        public WaveConfig[] Waves => _waves;
    }
}