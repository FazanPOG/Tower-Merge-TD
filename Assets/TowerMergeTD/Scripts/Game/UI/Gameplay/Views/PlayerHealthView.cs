using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        
        public void UpdateText(string text) => _healthText.text = text;
    }
}