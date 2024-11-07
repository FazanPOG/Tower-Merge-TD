using UnityEngine;

namespace TowerMergeTD.Game.UI.Root
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.Show();
        }
        
        public void HideLoadingScreen()
        {
            _loadingScreen.Hide();
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();
            sceneUI.transform.SetParent(_uiSceneContainer.transform, false);
        }

        private void ClearSceneUI()
        {
            for (int i = 0; i < _uiSceneContainer.childCount; i++)
            {
                var child = _uiSceneContainer.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}