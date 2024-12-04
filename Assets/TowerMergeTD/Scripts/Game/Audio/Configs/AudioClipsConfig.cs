using UnityEngine;

namespace TowerMergeTD.Game.Audio
{
    [CreateAssetMenu(menuName = "Configs/Audio/Clips")]
    public class AudioClipsConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _backgroundMusic;

        public AudioClip BackgroundMusic => _backgroundMusic;
    }
}