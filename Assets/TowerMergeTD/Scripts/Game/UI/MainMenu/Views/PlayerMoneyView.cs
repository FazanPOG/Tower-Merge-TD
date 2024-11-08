using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class PlayerMoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        public void UpdateText(string text) => _moneyText.text = text;
    }
}
