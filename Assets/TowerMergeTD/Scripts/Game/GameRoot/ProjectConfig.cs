using Sirenix.OdinInspector;
using TowerMergeTD.API;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/ProjectConfig", order = 0)]
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField] private bool _isDevelopmentSettings;
        [SerializeField, ShowIf(nameof(_isDevelopmentSettings))] private Language _language;
        [SerializeField] private TowerGenerationConfig[] _towerGenerations;
        [SerializeField] private Level[] _levels;

        public bool IsDevelopmentSettings => _isDevelopmentSettings;

        public Language Language => _language;
        
        public TowerGenerationConfig[] TowerGenerations => _towerGenerations;
        public Level[] Levels => _levels;
    }
}