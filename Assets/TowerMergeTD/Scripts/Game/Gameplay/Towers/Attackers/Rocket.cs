using System.Collections.Generic;
using TowerMergeTD.Game.Audio;
using Unity.Mathematics;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class Rocket : MonoBehaviour
    {
        private const float SFX_KOEF = 5;
        
        [SerializeField, Range(0f, 5f)] private float _damageRange;
        [SerializeField] private ParticleSystem _explosionEffectPrefab;

        private float _damage;
        private Transform _target;
        private AudioPlayer _audioPlayer;
        
        private List<Collider2D> _touchesColliders;

        public void Init(float damage, Transform target, AudioPlayer audioPlayer)
        {
            _damage = damage;
            _target = target;
            _audioPlayer = audioPlayer;
        }

        private void Update()
        {
            if (_target != null)
            {
                Move();
                Rotate();
                
                if (Vector2.Distance(transform.position, _target.position) < 0.25f)
                    Explode();
            }
            else
            {
                Explode();
            }
        }

        private void Explode()
        {
            var sfx = Instantiate(_explosionEffectPrefab, transform.position, quaternion.identity);
            sfx.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sfx.Play();
            DamageEnemies();
            _audioPlayer.Play(AudioType.Explosion, 0.15f);
            DestroySelf();
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