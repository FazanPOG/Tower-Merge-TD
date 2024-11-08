using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class PlayerBuildingCurrencyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valueText;

        public void SetValueText(string text) => _valueText.text = text;
    }
}