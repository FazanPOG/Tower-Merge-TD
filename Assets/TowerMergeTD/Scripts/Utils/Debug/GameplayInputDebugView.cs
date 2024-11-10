using TMPro;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Utils.Debug
{
    public class GameplayInputDebugView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _debugText;

        private IInput _input;
        
        public void Init(IInput input)
        {
            _input = input;
            
            _input.OnZoomIn += OnZoomIn;
            _input.OnZoomOut += OnZoomOut;
        }

        private void OnZoomOut(float obj)
        {
            _debugText.text = "Отдоление";
        }

        private void OnZoomIn(float obj)
        {
            _debugText.text = "Приближение";
        }
    }
}