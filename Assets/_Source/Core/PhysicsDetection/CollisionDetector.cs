using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
        
        [SerializeField] private LayerCollisionEvent[] _eventsOnTriggerEnter;
        [SerializeField] private LayerCollisionEvent[] _eventsOnTriggerExit;

        private readonly Dictionary<ITriggerEnterListener, CollisionSubscription> _triggerEnterListener = new();
        private readonly Dictionary<ITriggerExitListener, CollisionSubscription> _triggerExitListener = new();

        #region TriggerEnter
        
        public void Subscribe(ITriggerEnterListener enterListener, LayerMask layerMask, object additionalData = null)
        {
            _triggerEnterListener.Add(enterListener, new(layerMask, additionalData));
        }

        public void Subscribe(ITriggerEnterListener enterListener, object additionalData = null)
        {
            _triggerEnterListener.Add(enterListener, new(Physics.AllLayers, additionalData));
        }

        public void Unsubscribe(ITriggerEnterListener enterListener)
        {
            _triggerEnterListener.Remove(enterListener);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            foreach (var listener in _triggerEnterListener.ToArray())
            {
                if (listener.Value.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    listener.Key.TriggerEnter(other, listener.Value.AdditionalData);
                }
            }
            foreach (var eventOnCollision in _eventsOnTriggerEnter.ToArray())
            {
                if(eventOnCollision.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    eventOnCollision.Event?.Invoke();
                }
            }
        }
        
        #endregion
        
        #region TriggerExit
        
        public void Subscribe(ITriggerExitListener exitListener, LayerMask layerMask, object additionalData = null)
        {
            _triggerExitListener.Add(exitListener, new(layerMask, additionalData));
        }

        public void Subscribe(ITriggerExitListener exitListener, object additionalData = null)
        {
            _triggerExitListener.Add(exitListener, new(Physics.AllLayers, additionalData));
        }

        public void Unsubscribe(ITriggerExitListener exitListener)
        {
            _triggerExitListener.Remove(exitListener);
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (var listener in _triggerExitListener.ToArray())
            {
                if (listener.Value.LayerMask.ContainsLayer(other.gameObject.layer))
                {
                    listener.Key.TriggerExit(other, listener.Value.AdditionalData);
                }
            }
            foreach (var eventOnCollision in _eventsOnTriggerExit.ToArray())
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