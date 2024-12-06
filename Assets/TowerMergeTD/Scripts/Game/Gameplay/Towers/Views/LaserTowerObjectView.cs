using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class LaserTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform _towerTransform;
        [SerializeField] private GameObject _laser;
        [SerializeField] private SpriteRenderer _laserSpriteRenderer;
        [SerializeField] private AudioSource _audioSource;

        private TowerLaserAttacker _towerAttacker;
        private float _attackRange;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker, AudioPlayer audioPlayer)
        {
            base.Init(data, towerAttacker, audioPlayer);

            _attackRange = data.AttackRange;
            _towerAttacker = towerAttacker as TowerLaserAttacker;
        }

        protected override void TowerAttackerOnAttacked()
        {
            _audioSource.Play();
            _laser.gameObject.SetActive(true);
        }

        protected override void Update()
        {
            base.Update();
            
            if (_towerAttacker.ClosestTargetObject != null && _towerAttacker.OnDrag == false)
            {
                Vector3 direction = _towerAttacker.ClosestTargetObject.transform.position - _towerTransform.position;
                float distance = direction.magnitude;
                
                if (distance <= _attackRange)
                {
                    if (_laser.gameObject.activeSelf == false)
                    {
                        _audioSource.Play();
                        _laser.gameObject.SetActive(true);
                    }

                    _laserSpriteRenderer.size = new Vector2(_laserSpriteRenderer.size.x, distance / 3);
                    var scale = _laserSpriteRenderer.transform.localScale;
                    scale = new Vector3(scale.x, distance / 2.9f, scale.z);
                    _laserSpriteRenderer.transform.localScale = scale;
                }
                else
                {
                    _audioSource.Stop();
                    _laser.gameObject.SetActive(false);
                }
            }
            else
            {
                _audioSource.Stop();
                _laser.gameObject.SetActive(false);
            }
        }
    }
}
