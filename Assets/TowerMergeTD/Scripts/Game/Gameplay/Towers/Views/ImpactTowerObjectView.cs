using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class ImpactTowerObjectView : TowerObjectView
    {
        [SerializeField] private ParticleSystem _impactEffect;

        private AudioPlayer _audioPlayer;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker, AudioPlayer audioPlayer)
        {
            base.Init(data, towerAttacker, audioPlayer);
            
            _audioPlayer = audioPlayer;
        }

        protected override void TowerAttackerOnAttacked()
        {
            _impactEffect.Play();
            _audioPlayer.Play(AudioType.Impact);
        }

        protected override void Update() { }
    }
}