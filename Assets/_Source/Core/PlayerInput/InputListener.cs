using GameFeatures.ClickerFeature;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.PlayerInput
{
    public class InputListener : MonoBehaviour
    {
        private GameInputActions _gameInput;
        private Clicker _clicker;

        [Inject]
        public void Construct(Clicker clicker)
        {
            _clicker = clicker;
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
        }
    
        public void DisableInput()
        {
            _gameInput.Disable();
            _gameInput.GlobalActionMap.Work.performed -= Click;
        }

        private void Click(InputAction.CallbackContext callbackContext)
        {
            _clicker.Click();
        }
    }
}
