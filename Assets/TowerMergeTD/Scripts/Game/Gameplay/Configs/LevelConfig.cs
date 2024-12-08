using System;
using Sirenix.OdinInspector;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Game/Gameplay/GameConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [ToggleLeft]
        [LabelText("Level Unlocked")]
        [SerializeField] private bool _isOpen;
        
        [ToggleLeft]
        [LabelText("Is Tutorial Level")]
        [SerializeField] private bool _isTutorial;
        
        [FoldoutGroup("Initial Settings")]
        [LabelText("Starting Health")]
        [SerializeField] private int _initialHealth;
        
        [FoldoutGroup("Initial Settings", expanded: true)]
        [LabelText("Starting Building Currency")]
        [SerializeField] private int _initialBuildingCurrency;

        [FoldoutGroup("Star Points Requirements", expanded: true)]
        [LabelText("Points for 1 Star")]
        [SerializeField, Min(1)] private int scoreForOneStar;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("Points for 2 Stars")]
        [SerializeField, Min(2)] private int scoreForTwoStars;

        [FoldoutGroup("Star Points Requirements")]
        [LabelText("Points for 3 Stars")]
        [SerializeField, Min(3)] private int scoreForThreeStars;

        [Space(10)]
        [TabGroup("Configs", "Tile Set")]
        [HideLabel, InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        [SerializeField] private TileSetConfig _tileSetConfig;
        
        [TabGroup("Configs", "Waves")]
        [ListDrawerSettings(ShowPaging = true)]
        [Tooltip("Каждая WavesData представляет собой отдельный путь. Заполняйте массивы 'Waves' для настройки волн врагов по каждому пути. " +
                 "Количество WavesData ОБЯЗАТЕЛЬНО должно быть равно количетсву путей." +
                 "Количество Waves ДОЛЖНО быть одиннаковым")]
        [SerializeField] private WavesData[] _waveDatas;

        public bool IsOpen => _isOpen;
        
        public bool IsTutorial => _isTutorial;
        
        public int InitialBuildingCurrency => _initialBuildingCurrency;
        public int InitialHealth => _initialHealth;

        public int ScoreForOneStar => scoreForOneStar;
        public int ScoreForTwoStars => scoreForTwoStars;
        public int ScoreForThreeStars => scoreForThreeStars;

        public TileSetConfig TileSetConfig => _tileSetConfig;
        
        public WavesData[] WaveDatas => _waveDatas;

        private void OnValidate()
        {
            int previousCount = 0;
            foreach (var waveData in _waveDatas)
            {
                if (previousCount == 0)
                {
                    previousCount = waveData.Waves.Length;
                    continue;
                }

                if (waveData.Waves.Length != previousCount)
                    Debug.LogError($"The number of WaveConfig in each WaveData MUST be the same!");
            }
        }
    }

    [Serializable]
    public class WavesData
    {
        [SerializeField] private WaveConfig[] _waves;

        public WaveConfig[] Waves => _waves;
    }
}