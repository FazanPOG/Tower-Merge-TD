using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class ShopGemView : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}