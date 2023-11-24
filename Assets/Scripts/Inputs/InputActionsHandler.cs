using Assets.Scripts.BaseUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Inputs
{
    public class InputActionsHandler : MonoSingleton<InputActionsHandler>
    {
        private InputActions _inputActions;

        public Vector2 MousePosition;
        public bool IsMouseLeftButtonPressed = false;

        public Vector3 MouseWorldPosition 
        { 
            get
            {
                return Camera.main.ScreenToWorldPoint(MousePosition);
            } 
        }

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
            MousePosition = obj.ReadValue<Vector2>();
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