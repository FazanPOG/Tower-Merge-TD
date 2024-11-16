using System.Collections;
using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyDeathPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Animation _animation;

        public void Init(int buildingCurrencyValue)
        {
            _text.text = buildingCurrencyValue.ToString();
            _animation.Play();

            StartCoroutine(WaitUntilAnimationEnded());
        }

        private IEnumerator WaitUntilAnimationEnded()
        {
            yield return new WaitForSeconds(_animation.clip.length);
            Destroy(gameObject);
        }
    }
}