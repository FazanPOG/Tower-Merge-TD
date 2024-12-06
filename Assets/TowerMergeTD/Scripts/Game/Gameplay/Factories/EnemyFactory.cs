using System.Collections.Generic;
using TowerMergeTD.Game.Audio;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly Enemy _prefab;
        private readonly PlayerBuildingCurrencyProxy _playerBuildingCurrencyProxy;
        private readonly Transform _parent;
        private readonly AudioPlayer _audioPlayer;

        public EnemyFactory(
            DiContainer diContainer, 
            Enemy prefab, 
            PlayerBuildingCurrencyProxy playerBuildingCurrencyProxy, 
            Transform parent,
            AudioPlayer audioPlayer)
        {
            _diContainer = diContainer;
            _prefab = prefab;
            _playerBuildingCurrencyProxy = playerBuildingCurrencyProxy;
            _parent = parent;
            _audioPlayer = audioPlayer;
        }

        public Enemy Create(EnemyConfig config, List<Vector3> path, Vector2 position)
        {
            var instance = _diContainer.InstantiatePrefabForComponent<Enemy>(_prefab, _parent);
            instance.transform.position = position;
            instance.name = $"{config.name}";

            instance.Init(_playerBuildingCurrencyProxy, config, path, _audioPlayer);
            
            return instance;
        }
    }
}