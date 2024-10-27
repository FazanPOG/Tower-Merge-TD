using System;
using Game.State;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IDraggable
    {
        public event Action OnDroppedOnTileMap;
        public event Action<TowerObject> OnDroppedOnTower;
        void Init(Transform draggedTransform, Map baseMap, Map environmentMap, Tile[] towerPlaces, Tile[] environment);
        void StartDrag(Collider2D draggedObject);
        void DragPerformed(Vector3 dragWorldPosition);
        void Drop(Vector3 dropWorldPosition);
        void ResetPosition();
    }
}