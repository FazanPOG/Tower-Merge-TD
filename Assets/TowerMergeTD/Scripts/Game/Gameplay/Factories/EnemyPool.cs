using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyPool
    {
        private readonly DiContainer _diContainer;
        private readonly Enemy _prefab;
        private readonly Transform _parent;
        private ObjectPool<Enemy> _pool;

        public int CountActive => _pool.CountActive;
        
        public EnemyPool(DiContainer diContainer, Enemy prefab, Transform parent)
        {
            _diContainer = diContainer;
            _prefab = prefab;
            _parent = parent;

            _pool = new ObjectPool<Enemy>(
                CreateEnemy,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyEnemy,
                true,
                10,
                20
            );
        }

        public Enemy Get()
        {
            return _pool.Get();
        }

        public void Release(Enemy enemy)
        {
            _pool.Release(enemy);
        }

        private Enemy CreateEnemy()
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(_prefab, _parent);
            return enemy;
        }

        private void OnGetFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}