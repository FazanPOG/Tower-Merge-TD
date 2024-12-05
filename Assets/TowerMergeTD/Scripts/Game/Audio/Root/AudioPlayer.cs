using System;
using R3;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private IGameStateProvider _gameStateProvider;
        private AudioClipsConfig _clipsConfig;
        private AudioSource _audioSource;
        private IDisposable _disposable;
        
        public void Init(IGameStateProvider gameStateProvider, AudioClipsConfig clipsConfig)
        {
            if(_gameStateProvider != null || _clipsConfig != null)
                return;
            
            _gameStateProvider = gameStateProvider;
            _clipsConfig = clipsConfig;
            
            _audioSource = GetComponent<AudioSource>();
            
            _disposable = _gameStateProvider.GameState.SoundVolume.Subscribe(newValue =>
            {
                _audioSource.volume = newValue;
            });
        }

        public void Play(AudioType audioType) => PlaySound(audioType);

        public void Play(AudioType audioType, float volumeScale) => PlaySound(audioType, volumeScale);

        private void PlaySound(AudioType type, float volumeScale = 1f)
        {
            AudioClip clip;
            
            switch (type)
            {
                case AudioType.Cash:
                    clip = _clipsConfig.CashClip;
                    break;
                
                case AudioType.Button:
                    clip = _clipsConfig.ButtonClip;
                    break;
                
                default:
                    throw new NotImplementedException($"Audio type not implemented: {type}");
            }
            
            _audioSource.PlayOneShot(clip, volumeScale);
        }
        
        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}