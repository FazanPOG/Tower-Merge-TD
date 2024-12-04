using System;
using R3;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        private IGameStateProvider _gameStateProvider;
        private AudioSource _audioSource;
        private IDisposable _disposable;
        
        public void Init(IGameStateProvider gameStateProvider, AudioClipsConfig clipsConfig)
        {
            if(_gameStateProvider != null)
                return;
            
            _gameStateProvider = gameStateProvider;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = clipsConfig.BackgroundMusic;
            
            _disposable = _gameStateProvider.GameState.MusicVolume.Subscribe(newValue =>
            {
                _audioSource.volume = newValue;
            });

            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}