using System.Collections.Generic;
using Entitas;

namespace Inputs
{
    public class DestroyInputSystem: ICleanupSystem 
    {
        readonly IGroup<InputEntity> _group;
        readonly List<InputEntity> _buffer = new List<InputEntity>();

        public DestroyInputSystem(Contexts contexts) {
            _group = contexts.input.GetGroup(InputMatcher.CursorInput);
        }

        public void Cleanup() {
            foreach (var e in _group.GetEntities(_buffer)) {
                e.Destroy();
            }
        }
    }
}