using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Grids
{
    [Game, Event(EventTarget.Self)]
    public sealed class CellPosition : IComponent
    {
        public Vector2Int Value;
    }
}