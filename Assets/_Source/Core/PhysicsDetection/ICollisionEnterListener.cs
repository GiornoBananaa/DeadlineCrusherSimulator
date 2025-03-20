using UnityEngine;

namespace Core.PhysicsDetection
{
    public interface ICollisionEnterListener
    {
        void CollisionEnter(Collision other, object additionalData = null);
    }
}