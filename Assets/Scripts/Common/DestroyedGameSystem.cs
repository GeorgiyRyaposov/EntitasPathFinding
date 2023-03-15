using System.Collections.Generic;
using Entitas;

namespace Common
{
    public class DestroyedGameSystem : ReactiveSystem<GameEntity> 
    {
        public DestroyedGameSystem(Contexts contexts) : base(contexts.game) 
        {
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        { 
            return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Destroyed));
        }

        protected override bool Filter(GameEntity entity) => entity.isDestroyed;

        protected override void Execute(List<GameEntity> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var e = entities[i];
                foreach (var destroyedListener in e.destroyedListener.value)
                {
                    destroyedListener.OnDestroyed(e);
                }
                e.Destroy();
            }
        }
    }
}