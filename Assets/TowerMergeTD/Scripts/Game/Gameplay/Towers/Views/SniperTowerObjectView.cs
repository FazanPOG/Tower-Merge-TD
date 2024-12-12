using System.Collections;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class SniperTowerObjectView : TowerObjectView
    {
        [SerializeField] private GameObject _laserObject;
        [SerializeField] private SpriteRenderer _fireSpriteRenderer;

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
            StartCoroutine(ShowFireEffect());
        }
        
        private IEnumerator ShowFireEffect()
        {
            _fireSpriteRenderer.gameObject.SetActive(true);
            _audioPlayer.Play(AudioType.GunShot);
            yield return new WaitForSeconds(0.1f);
            _fireSpriteRenderer.gameObject.SetActive(false);
        }
    }
}
