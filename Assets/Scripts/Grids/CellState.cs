using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Grids
{
    [Game, Event(EventTarget.Self)]
    public class CellState : IComponent
    {
        public int Value;
    }

    public enum ECellState
    {
        None,
        Highlight,
        Error
    }
}