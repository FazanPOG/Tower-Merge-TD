using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/ProjectConfig", order = 0)]
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField] private LevelConfig[] _levelConfigs;

        public LevelConfig[] LevelConfigs => _levelConfigs;
    }
}