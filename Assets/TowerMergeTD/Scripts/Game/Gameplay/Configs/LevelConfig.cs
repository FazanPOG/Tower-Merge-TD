using Sirenix.OdinInspector;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [ToggleLeft]
        [LabelText("Level Unlocked")]
        [SerializeField] private bool _isOpen;

        [FoldoutGroup("Initial Settings", expanded: true)]
        [LabelText("Starting Health")]
        [SerializeField] private int _initialHealth;

        [FoldoutGroup("Initial Settings")]
        [LabelText("Starting Money")]
        [SerializeField] private int _initialMoney;

        [FoldoutGroup("Star Points Requirements", expanded: true)]
        [LabelText("Points for 1 Star")]
        [SerializeField] private int scoreForOneStar;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("Points for 2 Stars")]
        [SerializeField] private int scoreForTwoStars;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("Points for 3 Stars")]
        [SerializeField] private int scoreForThreeStars;

        [Space(10)]
        [TabGroup("Configs", "Tile Set")]
        [HideLabel, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        [SerializeField] private TileSetConfig _tileSetConfig;

        [TabGroup("Configs", "Tower Generation")]
        [ListDrawerSettings(Expanded = true, ShowPaging = true)]
        [SerializeField] private TowerGenerationConfig[] _towerGenerationConfigs;

        [TabGroup("Configs", "Waves")]
        [ListDrawerSettings(Expanded = true, ShowPaging = true)]
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