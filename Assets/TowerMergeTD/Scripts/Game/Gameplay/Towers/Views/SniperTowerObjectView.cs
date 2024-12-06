using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class SniperTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform _towerTransform;
        [SerializeField] private GameObject _laserObject;

        private AudioPlayer _audioPlayer;
        private float _attackRange;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker, AudioPlayer audioPlayer)
        {
            base.Init(data, towerAttacker, audioPlayer);

            _audioPlayer = audioPlayer;
            _attackRange = data.AttackRange;
            
            _laserObject.gameObject.SetActive(true);
            _laserObject.transform.localScale = new Vector3(_laserObject.transform.localScale.x, _attackRange, _laserObject.transform.localScale.z);
        }

        protected override void TowerAttackerOnAttacked()
        {
            _audioPlayer.Play(AudioType.SniperShot, 0.5f);
        }
    }
}
