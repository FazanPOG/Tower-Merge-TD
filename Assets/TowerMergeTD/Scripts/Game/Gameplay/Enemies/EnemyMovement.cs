using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyMovement
    {
        private MonoBehaviour _enemy;
        private Transform _view;
        private float _moveSpeed;
        private ObjectRotator _rotator;

        public void Move(MonoBehaviour enemy, Transform view, List<Vector3> path, float moveSpeed)
        {
            _enemy = enemy;
            _view = view;
            _moveSpeed = moveSpeed;
            _rotator = new ObjectRotator(view);
            _enemy.StartCoroutine(MoveRoutine(path));
        }
        
        private IEnumerator MoveRoutine(List<Vector3> path)
        {
            foreach (Vector3 targetPosition in path)
            {
                //TODO: smooth rotation
                _rotator.StartRotate(_enemy, targetPosition);
                
                while (Vector2.Distance(_enemy.transform.position, targetPosition) > 0.1f)
                {
                    _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                    yield return null;
                }
                
                _enemy.transform.position = targetPosition;
            }
        }

        private void Rotate(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - _view.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _view.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}