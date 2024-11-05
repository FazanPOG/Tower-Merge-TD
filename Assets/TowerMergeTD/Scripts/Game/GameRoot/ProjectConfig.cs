using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    [CreateAssetMenu(menuName = "Configs/ProjectConfig", order = 0)]
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField] private Level[] _levels;

        public Level[] Levels => _levels;
    }
}