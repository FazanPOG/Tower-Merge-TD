using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _initialHealth;
        [SerializeField] private int _initialMoney;
        [SerializeField] private WaveConfig[] _waves;

        public int InitialHealth => _initialHealth;
        public int InitialMoney => _initialMoney;

        public WaveConfig[] Waves => _waves;
    }
}