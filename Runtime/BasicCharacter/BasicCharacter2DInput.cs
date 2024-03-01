using MadeYellow.Character2D.Basic.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MadeYellow.Character2D.Basic
{
    /// <summary>
    /// Extension component that allows to recive input from user and transmit it to the character
    /// </summary>
    [RequireComponent(typeof(BasicCharacter2D))]
    public class BasicCharacter2DInput : MonoBehaviour
    {
        private BasicCharacter2D _character;
        private @BasicControls _inputs;
        
        private Vector2 _moveInput;

        private void Awake()
        {
            _character = GetComponent<BasicCharacter2D>();

            _inputs = new @BasicControls();
            _inputs.Character.Motion.performed += ctx => MoveInput(ctx);
            _inputs.Character.Motion.canceled += ctx => MoveInput(ctx);
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }

        /// <summary>
        /// Read motion input. If no input - transmit stop command
        /// </summary>
        /// <param name="context"></param>
        public void MoveInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _moveInput = context.ReadValue<Vector2>();
            }
            else if (context.canceled)
            {
                _moveInput = Vector2.zero;
            }

            _character.Move(_moveInput);
        }
    }
}