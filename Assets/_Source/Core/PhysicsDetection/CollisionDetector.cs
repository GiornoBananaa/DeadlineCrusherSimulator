using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Core.PhysicsDetection
{
    public class CollisionDetector : MonoBehaviour
    {
        [Serializable]
        protected class LayerCollisionEvent
        {
            public LayerMask LayerMask;
            public UnityEvent Event;
        }
        
        [Serializable]
        protected class CollisionSubscription
        {
            public LayerMask LayerMask;
            public object AdditionalData;

            public CollisionSubscription(LayerMask layerMask, object additionalData)
            {
                LayerMask = layerMask;
                AdditionalData = additionalData;
            }
        }
        
        [SerializeField] private LayerCollisionEvent[] _eventsOnCollisionEnter;
        [SerializeField] private LayerCollisionEvent[] _eventsOnCollisionExit;

        private readonly Dictionary<ICollisionEnterListener, CollisionSubscription> _collisionEnterListener = new();
        private readonly Dictionary<ICollisionExitListener, CollisionSubscription> _collisionExitListener = new();

        #region CollisionEnter
        
        public void Subscribe(ICollisionEnterListener enterListener, LayerMask layerMask, object additionalData = null)
        {
            _collisionEnterListener.Add(enterListener, new(layerMask, additionalData));
        }

        public void Subscribe(ICollisionEnterListener enterListener, object additionalData = null)
        {
            _collisionEnterListener.Add(enterListener, new(Physics.AllLayers, additionalData));
        }

        public void UnSubscribe(ICollisionEnterListener enterListener, object additionalData = null)
        {
            _collisionEnterListener.Remove(enterListener);
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var listener in _collisionEnterListener)
            {
                if (listener.Value.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    listener.Key.CollisionEnter(other, listener.Value.AdditionalData);
                }
            }
            foreach (var eventOnCollision in _eventsOnCollisionEnter)
            {
                if(eventOnCollision.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    eventOnCollision.Event?.Invoke();
                }
            }
        }
        
        #endregion
        
        #region CollisionExit
        
        public void Subscribe(ICollisionExitListener exitListener, LayerMask layerMask, object additionalData = null)
        {
            _collisionExitListener.Add(exitListener, new(layerMask, additionalData));
        }

        public void Subscribe(ICollisionExitListener exitListener, object additionalData = null)
        {
            _collisionExitListener.Add(exitListener, new(Physics.AllLayers, additionalData));
        }

        public void UnSubscribe(ICollisionExitListener exitListener, object additionalData = null)
        {
            _collisionExitListener.Remove(exitListener);
        }

        private void OnCollisionExit(Collision other)
        {
            foreach (var listener in _collisionExitListener)
            {
                if (listener.Value.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    listener.Key.CollisionExit(other, listener.Value.AdditionalData);
                }
            }
            foreach (var eventOnCollision in _eventsOnCollisionExit)
            {
                if(eventOnCollision.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    eventOnCollision.Event?.Invoke();
                }
            }
        }
        
        #endregion
    }
}