using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class ShopCoinView : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}