using Entitas;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public sealed class InputSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly ProjectInputActions _inputActions;

        private Vector2 _movement;
        
        public InputSystem(Contexts contexts)
        {
            _contexts = contexts;
            _inputActions = new ProjectInputActions();

            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMove;

            _inputActions.Player.Enable();
        }
        
        public void Execute()
        {
            UpdateCameraMovementValue();
        }

        private void UpdateCameraMovementValue()
        {
            var e = _contexts.input.CreateEntity();
            e.AddInputsCameraPositionInput(_movement);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.phase != InputActionPhase.Canceled 
                ? context.ReadValue<Vector2>() 
                : Vector2.zero;
        }
    }
}