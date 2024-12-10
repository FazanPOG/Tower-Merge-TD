using UnityEngine;

namespace TowerMergeTD.Game.Audio
{
    [CreateAssetMenu(menuName = "Configs/Game/Audio/Clips")]
    public class AudioClipsConfig : ScriptableObject
    {
        [Header("Music")]
        [SerializeField] private AudioClip _backgroundMusic;
        [Header("MainMenu")]
        [Space(5)]
        [SerializeField] private AudioClip _cashClip;
        [SerializeField] private AudioClip _buttonClip;
        [Header("Gameplay")]
        [Space(5)]
        [SerializeField] private AudioClip _12StarsWin;
        [SerializeField] private AudioClip _3StarsWin;
        [SerializeField] private AudioClip _enemyDeath1;
        [SerializeField] private AudioClip _enemyDeath2;
        [SerializeField] private AudioClip _tankDeath;
        [SerializeField] private AudioClip _gameOver;
        [SerializeField] private AudioClip _placeTower;
        [SerializeField] private AudioClip _mergeTowers;
        [SerializeField] private AudioClip _gunShot;
        [SerializeField] private AudioClip _laser;
        [SerializeField] private AudioClip _sniperShot;
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _electric;

        public AudioClip BackgroundMusic => _backgroundMusic;

        public AudioClip CashClip => _cashClip;

        public AudioClip ButtonClip => _buttonClip;

        public AudioClip OneTwoStarsWin => _12StarsWin;
        public AudioClip ThreeStarsWin => _3StarsWin;

        public AudioClip EnemyDeath1 => _enemyDeath1;

        public AudioClip EnemyDeath2 => _enemyDeath2;

        public AudioClip TankDeath => _tankDeath;

        public AudioClip GameOver => _gameOver;

        public AudioClip PlaceTower => _placeTower;

        public AudioClip MergeTowers => _mergeTowers;

        public AudioClip GunShot => _gunShot;

        public AudioClip Laser => _laser;
        
        public AudioClip SniperShot => _sniperShot;

        public AudioClip Explosion => _explosion;

        public AudioClip Electric => _electric;
    }
}