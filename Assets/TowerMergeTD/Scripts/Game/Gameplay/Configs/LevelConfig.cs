using Sirenix.OdinInspector;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private bool _isOpen;
        [SerializeField] private int _initialHealth;
        [SerializeField] private int _initialMoney;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("1 Star Points")]
        [SerializeField] private int scoreForOneStar;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("2 Stars Points")]
        [SerializeField] private int scoreForTwoStars;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("3 Stars Points")]
        [SerializeField] private int scoreForThreeStars;
        
        [SerializeField] private TileSetConfig _tileSetConfig;
        [SerializeField] private TowerGenerationConfig[] _towerGenerationConfigs;
        [SerializeField] private WaveConfig[] _waves;

        public bool IsOpen => _isOpen;
        public int InitialHealth => _initialHealth;
        public int InitialMoney => _initialMoney;

        public int ScoreForOneStar => scoreForOneStar;

        public int ScoreForTwoStars => scoreForTwoStars;

        public int ScoreForThreeStars => scoreForThreeStars;
        
        public TileSetConfig TileSetConfig => _tileSetConfig;
        
        public TowerGenerationConfig[] TowerGenerationConfigs => _towerGenerationConfigs;
        public WaveConfig[] Waves => _waves;
    }
}