using UnityEngine;

namespace Core.PhysicsDetection
{
    public interface ICollisionExitListener
    {
        void CollisionExit(Collision other, object additionalData = null);
    }
}