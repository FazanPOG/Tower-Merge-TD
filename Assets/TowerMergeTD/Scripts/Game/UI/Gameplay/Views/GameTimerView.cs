using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;

        public void SetTimeText(string text) => _timeText.text = text;
    }
}