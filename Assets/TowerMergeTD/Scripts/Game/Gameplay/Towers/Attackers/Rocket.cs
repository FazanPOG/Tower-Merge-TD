using System.Collections.Generic;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] private float _damageRange;

        private float _damage;
        private Transform _target;
        private List<Collider2D> _touchesColliders;

        public void Init(float damage, Transform target)
        {
            _damage = damage;
            _target = target;
        }

        private void Update()
        {
            if (_target != null)
            {
                Move();
                Rotate();
                
                if (Vector2.Distance(transform.position, _target.position) < 0.25f)
                {
                    DamageEnemies();
                    DestroySelf();
                }
            }
            else
            {
                DamageEnemies();
                DestroySelf();
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, 5f * Time.deltaTime);
        }
        
        private void Rotate()
        {
            Vector3 look = transform.InverseTransformPoint(_target.transform.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            transform.Rotate(0, 0, angle);
        }
        
        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void DamageEnemies()
        {
            Vector2 center = transform.position;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(center, _damageRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }
}