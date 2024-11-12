using System.Collections;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class GunTowerObjectView : TowerObjectView
    {
        [SerializeField] private SpriteRenderer _fireSpriteRenderer;

        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker)
        {
            base.Init(data, towerAttacker);
            _fireSpriteRenderer.gameObject.SetActive(false);
        }

        protected override void TowerAttackerOnAttacked()
        {
            StartCoroutine(ShowFireEffect());
        }
        
        private IEnumerator ShowFireEffect()
        {
            _fireSpriteRenderer.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _fireSpriteRenderer.gameObject.SetActive(false);
        }
    }
}