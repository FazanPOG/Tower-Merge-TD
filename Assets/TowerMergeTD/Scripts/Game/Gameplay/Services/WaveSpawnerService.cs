using System;
using System.Collections;
using System.Collections.Generic;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class WaveSpawnerService : IWaveSpawnerService
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly List<Vector3> _path;
        private readonly Vector2 _enemySpawnPosition;
        private readonly MonoBehaviour _monoBehaviour;

        public event Action OnWaveCompleted;
        public event Action OnAllWavesCompleted;

        private Queue<WaveConfig> _waves;
        private WaveConfig _currentWave;
        private Coroutine _coroutine;
        private int _enemyDiedCounter;
        
        public WaveSpawnerService(
            EnemyFactory enemyFactory, 
            WaveConfig[] waves, 
            List<Vector3> path, 
            Vector2 enemySpawnPosition,
            MonoBehaviourWrapper monoBehaviour)
        {
            _enemyFactory = enemyFactory;
            _path = path;
            _enemySpawnPosition = enemySpawnPosition;
            _monoBehaviour = monoBehaviour;

            _waves = new Queue<WaveConfig>(waves);
        }
        
        public void SpawnNextWave()
        {
            if(_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);

            if (_waves.Count > 0)
            {
                _currentWave = _waves.Dequeue();
                _coroutine = _monoBehaviour.StartCoroutine(SpawnEnemies(_currentWave));
            }
            else
            {
                OnAllWavesCompleted?.Invoke();
            }
        }

        private IEnumerator SpawnEnemies(WaveConfig wave)
        {
            List<Enemy> enemies = new List<Enemy>();
            
            foreach (var enemyConfig in wave.Enemies)
            {
                var enemy = _enemyFactory.Create(enemyConfig, _path, _enemySpawnPosition);
                enemies.Add(enemy);
                yield return new WaitForSeconds(wave.DelayBetweenEnemies);
            }

            foreach (var enemy in enemies)
                enemy.OnDied += OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            _enemyDiedCounter++;

            if (_enemyDiedCounter == _currentWave.Enemies.Length)
            {
                _enemyDiedCounter = 0;
                OnWaveCompleted?.Invoke();
            }
        }
    }
}