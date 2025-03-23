using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameFeatures.PlayerInput
{
    public class InputListener : MonoBehaviour
    {
        private GameInputActions _gameInput;
        private Clicker.ClickCounter _clickCounter;
        private RaycastInputListener _raycastInputListener;

        [Inject]
        public void Construct(Clicker.ClickCounter clickCounter, RaycastInputListener raycastInputListener)
        {
            _clickCounter = clickCounter;
            _raycastInputListener = raycastInputListener;
        }
    
        private void Awake()
        {
            _gameInput = new GameInputActions();
            _gameInput.Enable();
        }
        
        private void OnDestroy()
        {
            DisableInput();
        }

        public void EnableInput()
        {
            _gameInput.Enable();
            _gameInput.GlobalActionMap.Work.performed += Click;
            _gameInput.GlobalActionMap.RaycastInput.performed += RaycastInput;
        }
    
        public void DisableInput()
        {
            _gameInput.Disable();
            _gameInput.GlobalActionMap.Work.performed -= Click;
            _gameInput.GlobalActionMap.RaycastInput.performed -= RaycastInput;
        }

        private void Click(InputAction.CallbackContext callbackContext)
        {
            _clickCounter.Click();
        }
        
        private void RaycastInput(InputAction.CallbackContext callbackContext)
        {
            _raycastInputListener.RegisterInput();
        }
    }
}
