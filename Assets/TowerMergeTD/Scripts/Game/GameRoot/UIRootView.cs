using UnityEngine;

namespace TowerMergeTD.GameRoot
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.gameObject.SetActive(true);
        }
        
        public void HideLoadingScreen()
        {
            _loadingScreen.gameObject.SetActive(false);
        }

        public void AttackSceneUI(GameObject sceneUI)
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