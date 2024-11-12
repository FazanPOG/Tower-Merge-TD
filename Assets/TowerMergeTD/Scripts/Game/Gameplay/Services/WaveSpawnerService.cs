using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class WaveSpawnerService : IWaveSpawnerService, IPauseHandler
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
        private bool _canSpawn;

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
            _canSpawn = true;
        }

        public void HandlePause(bool isPaused) => _canSpawn = !isPaused;

        public void SpawnNextWave()
        {
            if(_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);

            if (_waves.Count > 0)
            {
                _currentWave = _waves.Dequeue();
                if(_currentWave.Enemies.IsEmpty())
                    OnWaveCompleted?.Invoke();
                else
                    _coroutine = _monoBehaviour.StartCoroutine(SpawnEnemies(_currentWave));
            }
            else
            {
                OnAllWavesCompleted?.Invoke();
            }
        }

        private IEnumerator SpawnEnemies(WaveConfig wave)
        {
            foreach (var enemyConfig in wave.Enemies)
            {
                yield return new WaitUntil(() => _canSpawn);
            
                var enemy = _enemyFactory.Create(enemyConfig, _path, _enemySpawnPosition);
                enemy.OnDied += OnEnemyDied;
                
                yield return new WaitForSeconds(wave.DelayBetweenEnemies);
            }
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