using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyFactory
    {
        private EnemyPool _pool;

        public EnemyFactory(DiContainer diContainer, Enemy prefab, Transform parent)
        {
            _pool = new EnemyPool(diContainer, prefab, parent);
        }

        public Enemy Create(EnemyConfig config, List<Vector3> path, Vector2 position)
        {
            var instance = _pool.Get();
            instance.transform.position = position;
            instance.name = $"{config.name}";

            instance.Init(config, path);
            
            return instance;
        }

        public void Release(Enemy enemy)
        {
            _pool.Release(enemy);
        }
    }
}