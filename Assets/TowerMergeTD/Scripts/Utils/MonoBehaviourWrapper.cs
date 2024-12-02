using System;
using UnityEngine;

namespace TowerMergeTD.Utils
{
    public class MonoBehaviourWrapper : MonoBehaviour
    {
        public event Action OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}