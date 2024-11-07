using UnityEngine;

namespace TowerMergeTD.Game.UI.Root
{
    public class LoadingScreen : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}