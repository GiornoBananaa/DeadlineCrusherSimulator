using UnityEngine;

namespace Core.PhysicsDetection
{
    public interface ITriggerExitListener
    {
        void TriggerExit(Collider other, object additionalData = null);
    }
}