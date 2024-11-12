using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class EpisodeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _episodeText;

        public void SetEpisodeNumberText(int number)
        {
            _episodeText.text = $"Эпизод {number}";
        }
    }
}