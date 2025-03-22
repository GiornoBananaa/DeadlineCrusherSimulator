using UnityEngine;

namespace Core.PhysicsDetection
{
    public interface ITriggerEnterListener
    {
        void TriggerEnter(Collider other, object additionalData = null);
    }
}