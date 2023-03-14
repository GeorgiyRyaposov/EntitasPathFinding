using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Inputs
{
    [Input, Unique, Cleanup(CleanupMode.DestroyEntity)]
    public sealed class CameraPositionInput : IComponent
    {
        public Vector2 Value;
    }
}