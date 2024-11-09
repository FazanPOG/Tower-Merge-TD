using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class ShopTowersView : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}