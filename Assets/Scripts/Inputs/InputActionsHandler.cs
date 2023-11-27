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

        public Vector2 DirInput;

        public Vector3 MouseWorldPosition 
        { 
            get
            {
                return Camera.main.ScreenToWorldPoint(MousePosition);
            } 
        }

        public Vector2 getDirInput()
        {
            return DirInput;
        }

        private void OnEnable()
        {
            _inputActions = InputActions.Instance;

            _inputActions.Player.MousePosition.performed += OnMousePositionPerformed;
            _inputActions.Player.MouseAction.performed += OnMouseActionPerformed;
            _inputActions.Player.MouseAction.canceled += OnMouseActionCanceled;

            _inputActions.Player.MoveXY.performed += OnDirInput;
            _inputActions.Player.MoveXY.started += OnDirInputStart;
            _inputActions.Player.MoveXY.canceled += OnDirInputCancel;
        }

        private void OnDisable()
        {
            _inputActions.Player.MousePosition.performed -= OnMousePositionPerformed;
            _inputActions.Player.MouseAction.performed -= OnMouseActionPerformed;
            _inputActions.Player.MouseAction.canceled -= OnMouseActionCanceled;

            _inputActions.Player.MoveXY.performed -= OnDirInput;
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

        private void OnDirInput(InputAction.CallbackContext obj)
        {
            DirInput = obj.ReadValue<Vector2>();
        }
        private void OnDirInputStart(InputAction.CallbackContext obj)
        {
            DirInput = obj.ReadValue<Vector2>();
        }

        private void OnDirInputCancel(InputAction.CallbackContext obj)
        {
            DirInput = Vector2.zero;
        }
    }
}