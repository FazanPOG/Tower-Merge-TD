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
                
                case AudioType.GameOver:
                    clip = _clipsConfig.GameOver;
                    break;
                
                case AudioType.OneTwoStarsWin:
                    clip = _clipsConfig.OneTwoStarsWin;
                    break;
                
                case AudioType.ThreeStarsWin:
                    clip = _clipsConfig.ThreeStarsWin;
                    break;
                
                case AudioType.EnemyDeathFirstSound:
                    clip = _clipsConfig.EnemyDeath1;
                    break;
                
                case AudioType.EnemyDeathSecondSound:
                    clip = _clipsConfig.EnemyDeath2;
                    break;
                
                case AudioType.PlaceTower:
                    clip = _clipsConfig.PlaceTower;
                    break;
                
                case AudioType.MergeTowers:
                    clip = _clipsConfig.MergeTowers;
                    break;
                
                case AudioType.GunShot:
                    clip = _clipsConfig.GunShot;
                    break;
                
                case AudioType.Laser:
                    clip = _clipsConfig.Laser;
                    break;
                
                case AudioType.SniperShot:
                    clip = _clipsConfig.SniperShot;
                    break;
                
                case AudioType.TankDeath:
                    clip = _clipsConfig.TankDeath;
                    break;
                
                case AudioType.Explosion:
                    clip = _clipsConfig.Explosion;
                    break;
                
                case AudioType.Electric:
                    clip = _clipsConfig.Electric;
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