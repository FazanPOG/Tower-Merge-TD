using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class EpisodeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _episodeText;

        public void SetEpisodeNumberText(string text) => _episodeText.text = text;
    }
}