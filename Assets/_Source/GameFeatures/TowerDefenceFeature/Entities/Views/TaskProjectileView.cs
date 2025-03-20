using Core.EntitySystem;
using Core.PhysicsDetection;
using UnityEngine;

namespace GameFeatures.TowerDefenceFeature
{
    public class TaskProjectileView : GameEntityLinker
    {
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
    }
}