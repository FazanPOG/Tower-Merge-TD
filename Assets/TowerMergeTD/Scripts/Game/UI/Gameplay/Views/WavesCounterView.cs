using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class WavesCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;

        public void SetCountText(string text) => _countText.text = text;
    }
}