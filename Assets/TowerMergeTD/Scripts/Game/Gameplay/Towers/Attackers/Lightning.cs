using System.Collections.Generic;
using TowerMergeTD.Game.Audio;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class Lightning : MonoBehaviour
    {
        private const int MAX_DAMAGE_ENEMIES_COUNT = 5;
        
        [SerializeField, Range(0f, 5f)] private float _bounceRadius;

        private float _damage;
        private Transform _currentTarget;
        private IDamageable _currentIDamageable;
        private int _damagedEnemiesCount;
        private AudioPlayer _audioPlayer;

        private List<Collider2D> _touchesColliders;

        public void Init(float damage, Transform target, AudioPlayer audioPlayer)
        {
            _damage = damage;
            _currentTarget = target;
            _audioPlayer = audioPlayer;
            _currentIDamageable = _currentTarget.GetComponent<IDamageable>();
            _damagedEnemiesCount = 0;
        }

        private void Update()
        {
            if (_currentTarget != null)
            {
                Move();
                Rotate();
                
                if (Vector2.Distance(transform.position, _currentTarget.position) < 0.2f)
                    LightningStrike();
            }
            else
            {
                DestroySelf();
            }
        }

        private void LightningStrike()
        {
            _currentIDamageable.TakeDamage(_damage);
            _audioPlayer.Play(AudioType.Electric);
            
            _damagedEnemiesCount++;

            if(_damagedEnemiesCount >= MAX_DAMAGE_ENEMIES_COUNT)
                DestroySelf();
            else
                FindNextEnemy();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, 5f * Time.deltaTime);
        }
        
        private void Rotate()
        {
            Vector3 look = transform.InverseTransformPoint(_currentTarget.transform.position);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            transform.Rotate(0, 0, angle);
        }
        
        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void FindNextEnemy()
        {
            Vector2 center = transform.position;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(center, _bounceRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out IDamageable damageable) && collider.gameObject.transform != _currentTarget)
                {
                    _currentTarget = collider.gameObject.transform;
                    _currentIDamageable = damageable;
                    return;
                }
            }

            _currentIDamageable = null;
            _currentTarget = null;
        }
    }
}