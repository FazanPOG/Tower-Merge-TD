using UnityEngine;

namespace TowerMergeTD.Game.Audio
{
    [CreateAssetMenu(menuName = "Configs/Audio/Clips")]
    public class AudioClipsConfig : ScriptableObject
    {
        [Header("Music")]
        [SerializeField] private AudioClip _backgroundMusic;
        [Header("MainMenu")]
        [Space(5)]
        [SerializeField] private AudioClip _cashClip;
        [SerializeField] private AudioClip _buttonClip;

        public AudioClip BackgroundMusic => _backgroundMusic;

        public AudioClip CashClip => _cashClip;

        public AudioClip ButtonClip => _buttonClip;
    }
}