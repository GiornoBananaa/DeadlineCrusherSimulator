using Core.EntitySystem;
using Core.PhysicsDetection;
using UnityEngine;

namespace GameFeatures.WorkProgress
{
    public class ExpirationLineView : GameEntityLinker
    {
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
    }
}