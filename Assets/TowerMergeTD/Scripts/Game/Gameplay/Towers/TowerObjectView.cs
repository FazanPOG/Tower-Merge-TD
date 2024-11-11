using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerObjectView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _objectToRotate;

        public GameObject ObjectToRotate => _objectToRotate;

        public void Init(TowerDataProxy data)
        {
            _spriteRenderer.sprite = data.Sprite;
        }
    }
}
