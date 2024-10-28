using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _initialHealth;
        [SerializeField] private WaveConfig[] _waves;

        public int InitialHealth => _initialHealth;

        public WaveConfig[] Waves => _waves;
    }
}