using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Inputs
{
    [Input, Unique, Cleanup(CleanupMode.DestroyEntity)]
    public class CursorInput
    {
        public Vector2Int Position;
        public bool Pressed;
        public bool OverUI;
    }
}