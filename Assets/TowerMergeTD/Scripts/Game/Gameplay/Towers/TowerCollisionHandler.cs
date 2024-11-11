using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class TowerCollisionHandler : MonoBehaviour
    {
        private BoxCollider2D _dragAndDropCollider;
        private CircleCollider2D _attackCollider;

        public event Action<Collider2D> OnDragAndDropColliderTriggering;
        public event Action<List<Collider2D>> OnAttackColliderTriggering;

        public CircleCollider2D AttackCollider => _attackCollider;

        private void Awake()
        {
            _dragAndDropCollider = GetComponent<BoxCollider2D>();
            _attackCollider = GetComponent<CircleCollider2D>();
            
            _attackCollider.isTrigger = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_dragAndDropCollider.IsTouching(other))
            {
                OnDragAndDropColliderTriggering?.Invoke(other);
            }
            
            if (_attackCollider.IsTouching(other))
            {
                List<Collider2D> results = new List<Collider2D>();
                _attackCollider.OverlapCollider(new ContactFilter2D(), results);
                OnAttackColliderTriggering?.Invoke(results);
            }
        }
    }
}