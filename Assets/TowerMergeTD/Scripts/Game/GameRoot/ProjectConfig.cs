using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/ProjectConfig", order = 0)]
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField] private bool _isDevelopmentSettings;
        [SerializeField] private TowerGenerationConfig[] _towerGenerations;
        [SerializeField] private Level[] _levels;

        public bool IsDevelopmentSettings => _isDevelopmentSettings;

        public TowerGenerationConfig[] TowerGenerations => _towerGenerations;
        public Level[] Levels => _levels;
    }
}