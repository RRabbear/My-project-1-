using Assets.Scripts.BaseUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Inputs
{
    public class InputActionsHandler : MonoSingleton<InputActionsHandler>
    {
        private InputActions _inputActions;

        public Vector2 MousePostion;
        public bool IsMouseLeftButtonPressed = false;

        private void OnEnable()
        {
            _inputActions = InputActions.Instance;

            _inputActions.Player.MousePosition.performed += OnMousePositionPerformed;
            _inputActions.Player.MouseAction.performed += OnMouseActionPerformed;
            _inputActions.Player.MouseAction.canceled += OnMouseActionCanceled;
        }

        private void OnDisable()
        {
            _inputActions.Player.MousePosition.performed -= OnMousePositionPerformed;
            _inputActions.Player.MouseAction.performed -= OnMouseActionPerformed;
            _inputActions.Player.MouseAction.canceled -= OnMouseActionCanceled;
        }

        private void OnMousePositionPerformed(InputAction.CallbackContext obj)
        {
            MousePostion = obj.ReadValue<Vector2>();
        }

        private void OnMouseActionPerformed(InputAction.CallbackContext obj)
        {
            IsMouseLeftButtonPressed = true;
        }

        private void OnMouseActionCanceled(InputAction.CallbackContext obj)
        {
            IsMouseLeftButtonPressed = false;
        }
    }
}