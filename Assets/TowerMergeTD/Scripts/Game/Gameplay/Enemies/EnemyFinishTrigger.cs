using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyFinishTrigger : MonoBehaviour
    {
        private PlayerHealthProxy _playerHealth;
        
        public void Init(PlayerHealthProxy playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                _playerHealth.Health.Value -= enemy.Damage;
                enemy.DestroySelf();
            }
        }
    }
}