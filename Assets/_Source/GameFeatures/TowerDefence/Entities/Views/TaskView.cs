using Core.EntitySystem;
using Core.PhysicsDetection;
using UnityEngine;

namespace GameFeatures.TowerDefence
{
    public class TaskView : GameEntityLinker
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
        
        [field: SerializeField] public Transform ShootPoint { get; private set; }
        
        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}