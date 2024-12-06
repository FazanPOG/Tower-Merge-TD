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
        private bool _isPlaying;
        
        public void Init(IGameStateProvider gameStateProvider, AudioClip clip)
        {
            _gameStateProvider = gameStateProvider;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = clip;
            
            _disposable = _gameStateProvider.GameState.MusicVolume.Subscribe(newValue =>
            {
                _audioSource.volume = newValue;
            });

            _audioSource.loop = true;

            if (_isPlaying == false)
            {
                _audioSource.Play();
                _isPlaying = true;
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}