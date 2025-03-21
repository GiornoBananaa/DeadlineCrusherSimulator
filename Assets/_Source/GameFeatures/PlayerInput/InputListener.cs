using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GameFeatures.PlayerInput
{
    public class InputListener : MonoBehaviour
    {
        private GameInputActions _gameInput;
        private Clicker.Clicker _clicker;
        private RaycastInputListener _raycastInputListener;

        [Inject]
        public void Construct(Clicker.Clicker clicker, RaycastInputListener raycastInputListener)
        {
            _clicker = clicker;
            _raycastInputListener = raycastInputListener;
        }
    
        private void Awake()
        {
            _gameInput = new GameInputActions();
            _gameInput.Enable();
            EnableInput();
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
            _clicker.Click();
        }
        
        private void RaycastInput(InputAction.CallbackContext callbackContext)
        {
            _raycastInputListener.RegisterInput();
        }
    }
}
