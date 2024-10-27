using System.Collections;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class ObjectRotator
    {
        private const float ROTATION_SPEED = .5f;
        
        private readonly Transform _transformToRotate;

        private MonoBehaviour _currentContext;
        private Coroutine _coroutine;
        private Quaternion _defaultRotation;
        
        public ObjectRotator(Transform transformToRotate)
        {
            _transformToRotate = transformToRotate;
            _defaultRotation = _transformToRotate.rotation;
        }
        
        public void StartRotate(MonoBehaviour context, Vector3 targetPosition)
        {
            StopRotate();
            
            _currentContext = context;
            _currentContext.StartCoroutine(Rotate(targetPosition));
        }

        private void StopRotate()
        {
            if(_coroutine != null && _currentContext != null)
                _currentContext.StopCoroutine(_coroutine);

            _currentContext = null;
            _coroutine = null;

            _transformToRotate.rotation = _defaultRotation;
        }
        
        private IEnumerator Rotate(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _transformToRotate.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _transformToRotate.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
        
        private IEnumerator RotateSmooth(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _transformToRotate.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            while (Quaternion.Angle(_transformToRotate.rotation, targetRotation) > 0.1f)
            {
                _transformToRotate.rotation = Quaternion.RotateTowards(
                    _transformToRotate.rotation,
                    targetRotation,
                    ROTATION_SPEED * Time.deltaTime
                );
                yield return null;
            }

            _transformToRotate.rotation = targetRotation;
        }
    }
}