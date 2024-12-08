using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Game/Gameplay/Wave", order = 0)]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField, Range(0, 10)] private float _delayBetweenEnemies;
        [SerializeField] private EnemyConfig[] _enemies;

        public float DelayBetweenEnemies => _delayBetweenEnemies;
        public EnemyConfig[] Enemies => _enemies;
    }
}