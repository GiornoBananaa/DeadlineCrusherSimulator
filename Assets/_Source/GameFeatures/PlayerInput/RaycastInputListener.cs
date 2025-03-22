using System;
using UnityEngine;

namespace GameFeatures.PlayerInput
{
    public class RaycastInputListener : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        private RaycastInputReceiver _lastReceiver;

        private void Update()
        {
            CheckRaycastPoint();
        }

        public void RegisterInput()
        {
            RaycastInputReceiver receiver = GetCurrentReceiver(out RaycastHit hit);
            if(receiver == null) return;
            receiver.ReceiveInput(hit.point);
        }
        
        private void CheckRaycastPoint()
        {
            RaycastInputReceiver receiver = GetCurrentReceiver(out RaycastHit hit);
            if(receiver == null) return;
            receiver.ReceiveRayPoint(hit.point);
        }
        
        private RaycastInputReceiver GetCurrentReceiver(out RaycastHit hit)
        {
            if (!Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
                return null;
            if(_lastReceiver && hit.collider.gameObject == _lastReceiver.gameObject)
                return _lastReceiver;
            _lastReceiver = hit.collider.GetComponent<RaycastInputReceiver>();
            return _lastReceiver;
        }
    }
}