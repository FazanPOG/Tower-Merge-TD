using System.Collections;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerObjectView : MonoBehaviour
    {
        private const float ROTATION_SPEED = 100f;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _objectToRotate;
        [SerializeField] private SpriteRenderer _fireSpriteRenderer;
        
        private bool _isRotate;
        private Transform _currentRotateTarget;
        

        public void Init(TowerDataProxy data, ITowerAttacker towerAttacker)
        {
            _spriteRenderer.sprite = data.Sprite;
            _fireSpriteRenderer.gameObject.SetActive(false);
            
            towerAttacker.OnTargetChanged += StartRotation;
            towerAttacker.OnAttacked += TriggerFireEffect;
        }

        private void StartRotation(GameObject target)
        {
            _currentRotateTarget = target.transform;
            _isRotate = true;
        }

        private void TriggerFireEffect()
        {
            StartCoroutine(ShowFireEffect());
        }

        private IEnumerator ShowFireEffect()
        {
            _fireSpriteRenderer.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _fireSpriteRenderer.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (_isRotate && _currentRotateTarget.transform != null)
            {
                Vector3 look = _objectToRotate.transform.InverseTransformPoint(_currentRotateTarget.transform.position);
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
                
                _objectToRotate.transform.Rotate(0, 0, angle);
            }
        }
    }
}
