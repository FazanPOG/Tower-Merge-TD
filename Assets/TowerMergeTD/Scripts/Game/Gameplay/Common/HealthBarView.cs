using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.Gameplay
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _barImage;

        public void UpdateBarFillAmount(float fillAmount)
        {
            _barImage.fillAmount = fillAmount;
        }
    }
}