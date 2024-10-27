using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Utils
{
    public class MonoBehaviourWrapper : MonoBehaviour
    {
        private List<ITickable> _tickables = new List<ITickable>();
        
        public void AttachTickableList(List<ITickable> tickables)
        {
            _tickables = tickables;
        }
        
        public void AddTickableToList(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void ClearTickableList()
        {
            _tickables.Clear();
        }
        
        private void Update()
        {
            foreach (var tickable in _tickables)
            {
                tickable.Tick();
            }
        }
    }
}