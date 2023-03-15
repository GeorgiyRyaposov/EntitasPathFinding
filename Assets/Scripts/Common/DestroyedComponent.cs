using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Common
{
    [Game, Event(EventTarget.Self), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class DestroyedComponent : IComponent { }
}