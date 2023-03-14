using Cameras;
using Entitas;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public sealed class InputSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly ProjectInputActions _inputActions;
        private readonly ICameraView _cameraView;

        private Vector2 _movement;
        private Vector2 _cursorPosition;
        private bool _cursorPressed;

        public InputSystem(Contexts contexts)
        {
            _contexts = contexts;

            _cameraView = _contexts.config.gameSceneArguments.value.CameraView;
            _inputActions = new ProjectInputActions();

            _inputActions.Player.CameraMove.performed += OnMove;
            _inputActions.Player.CameraMove.canceled += OnMove;
            
            _inputActions.Player.Cursor.started += OnCursor;
            _inputActions.Player.Cursor.performed += OnCursor;
            _inputActions.Player.Cursor.canceled += OnCursor;

            _inputActions.Player.Enable();
        }
        
        public void Execute()
        {
            UpdateCameraMovementValue();
            UpdateCursorInput();
        }

        private void UpdateCameraMovementValue()
        {
            var e = _contexts.input.CreateEntity();
            e.AddInputsCameraPositionInput(_movement);
        }

        private void UpdateCursorInput()
        {
            var mouseWorldPos = _cameraView.ScreenToWorldPoint(_cursorPosition);
            
            var e = _contexts.input.CreateEntity();
            e.AddCursorInput(new CursorInput()
            {
                Position = new Vector2Int(
                    Mathf.RoundToInt(mouseWorldPos.x),
                    Mathf.RoundToInt(mouseWorldPos.z)
                ),
                Pressed = _cursorPressed,
            });
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.phase != InputActionPhase.Canceled 
                ? context.ReadValue<Vector2>() 
                : Vector2.zero;
        }

        private void OnCursor(InputAction.CallbackContext context)
        {
            _cursorPosition = context.ReadValue<Vector2>();
            
            var buttonDown = false;
            if (context.control.device is Mouse mouse)
            {
                buttonDown = mouse.leftButton.isPressed;
            }

            _cursorPressed = buttonDown;
        }
    }
}