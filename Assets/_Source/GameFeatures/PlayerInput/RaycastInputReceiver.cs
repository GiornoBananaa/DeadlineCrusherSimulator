using System;
using UnityEngine;

namespace GameFeatures.PlayerInput
{
    public class RaycastInputReceiver : MonoBehaviour
    {
        public event Action<Vector3> OnRayPointedStarted;
        public event Action<Vector3> OnRayPointed;
        public event Action<Vector3> OnRayPointedEnd;
        public event Action<Vector3> OnInputStarted;
        public event Action<Vector3,Vector3> OnInput;
        public event Action<Vector3> OnInputEnded;

        private Vector3 _lastRayPoint;
        private bool _receivedRayPointInFrame;
        private bool _receivingRayPoint;
        private Vector3 _lastHitPosition;
        private bool _receivedInputInFrame;
        private bool _receivingInput;

        private void LateUpdate()
        {
            UpdateInputReceive();
            UpdateRayPointReceive();
        }
        
        public void ReceiveRayPoint(Vector3 hitPosition)
        {
            OnRayPointed?.Invoke(hitPosition);
            
            if(!_receivingRayPoint)
                OnRayPointedStarted?.Invoke(hitPosition);
            else if (_receivingInput)
                OnRayPointed?.Invoke(hitPosition);

            _receivingRayPoint = true;
            _receivedRayPointInFrame = true;
            _lastRayPoint = hitPosition;
        }
        
        public void ReceiveInput(Vector3 hitPosition)
        {
            if(!_receivingInput)
                OnInputStarted?.Invoke(hitPosition);
            else if (_receivingInput)
                OnInput?.Invoke(_lastHitPosition, hitPosition);

            _receivingInput = true;
            _receivedInputInFrame = true;
            _lastHitPosition = hitPosition;
        }

        private void UpdateInputReceive()
        {
            if (_receivingInput && !_receivedInputInFrame)
            {
                _receivingInput = false;
                OnInputEnded?.Invoke(_lastHitPosition);
            }

            _receivedInputInFrame = false;
        }
        
        private void UpdateRayPointReceive()
        {
            if (_receivingRayPoint && !_receivedRayPointInFrame)
            {
                _receivingRayPoint = false;
                OnRayPointedEnd?.Invoke(_lastRayPoint);
            }

            _receivedRayPointInFrame = false;
        }
    }
}