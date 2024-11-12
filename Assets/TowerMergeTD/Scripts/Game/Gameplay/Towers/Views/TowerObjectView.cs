using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public abstract class TowerObjectView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _towerSpriteRenderer;
        [SerializeField] private GameObject _objectToRotate;
        
        private bool _isRotate;
        private Transform _currentRotateTarget;
        private ITowerAttacker _towerAttacker;

        protected TowerDataProxy _towerData;
        
        public virtual void Init(TowerDataProxy data, ITowerAttacker towerAttacker)
        {
            _towerData = data;
            _towerAttacker = towerAttacker;
            
            _towerSpriteRenderer.sprite = _towerData.Sprite;

            _towerAttacker.OnTargetChanged += StartRotation;
            _towerAttacker.OnAttacked += TowerAttackerOnAttacked;
        }

        private void StartRotation(GameObject target)
        {
            _currentRotateTarget = target.transform;
            _isRotate = true;
        }

        protected abstract void TowerAttackerOnAttacked();

        private void Update()
        {
            if (_isRotate && _currentRotateTarget != null)
            {
                Vector3 look = _objectToRotate.transform.InverseTransformPoint(_currentRotateTarget.transform.position);
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
                
                _objectToRotate.transform.Rotate(0, 0, angle);
            }
        }

        private void OnDestroy()
        {
            _towerAttacker.OnTargetChanged -= StartRotation;
            _towerAttacker.OnAttacked -= TowerAttackerOnAttacked;
        }
    }
}
