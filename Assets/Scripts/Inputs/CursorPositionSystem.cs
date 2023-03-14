using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Inputs
{
    public class CursorPositionSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _characters;

        public CursorPositionSystem(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
            _characters = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character));
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        { 
            return context.CreateCollector(InputMatcher.CursorInput);
        }

        protected override bool Filter(InputEntity entity) => entity.hasCursorInput;

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                var activeCharacter = GetActiveCharacter();
                
                var position = entity.cursorInput.value.Position;
                var followPath = entity.cursorInput.value.Pressed;
                var cell = _contexts.game.GetCellWithPosition(position);
                if (cell != null)
                {
                    var findPathEntity = _contexts.game.CreateEntity();
                    findPathEntity.AddFindPathRequest(activeCharacter.cellPosition.Value, position, followPath);
                }
            }
        }

        private GameEntity GetActiveCharacter()
        {
            foreach (var character in _characters)
            {
                if (character.character.Active)
                {
                    return character;
                }
            }

            Debug.LogError($"<color=red>Failed to find active character</color>");
            return null;
        }
    }
}