using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/ProjectConfig", order = 0)]
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField] private bool _isDevelopmentSettings;
        [SerializeField] private Level[] _levels;

        public bool IsDevelopmentSettings => _isDevelopmentSettings;
        public Level[] Levels => _levels;
    }
}