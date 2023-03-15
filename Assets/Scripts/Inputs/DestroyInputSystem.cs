using System.Collections.Generic;
using Entitas;

namespace Inputs
{
    public class DestroyInputSystem : ReactiveSystem<InputEntity> 
    {
        public DestroyInputSystem(Contexts contexts) : base(contexts.input) 
        {
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        { 
            return context.CreateCollector(InputMatcher.CursorInput);
        }

        protected override bool Filter(InputEntity entity) => true;

        protected override void Execute(List<InputEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Destroy();
            }
        }
    }
}