using System.Collections;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class GunTowerObjectView : TowerObjectView
    {
        [SerializeField] private SpriteRenderer _fireSpriteRenderer;

        private AudioPlayer _audioPlayer;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker, AudioPlayer audioPlayer)
        {
            base.Init(data, towerAttacker, audioPlayer);
            
            _fireSpriteRenderer.gameObject.SetActive(false);
            _audioPlayer = audioPlayer;
        }

        protected override void TowerAttackerOnAttacked()
        {
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