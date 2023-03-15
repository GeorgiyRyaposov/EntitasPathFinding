using System.Collections.Generic;
using Entitas;
using Game;
using UnityEngine;

namespace Characters
{
    public class SetCharacterSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _characters;

        public SetCharacterSystem(Contexts contexts) : base(contexts.input)
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
                if (!entity.cursorInput.value.Pressed || entity.cursorInput.value.OverUI)
                {
                    continue;
                }

                var position = entity.cursorInput.value.Position;
                
                var cell = _contexts.game.GetCellWithPosition(position);
                if (cell == null)
                {
                    //clicked out of grid
                    break;
                }
                
                var character = GetCharacter(position);
                if (character != null)
                {
                    //clicked at character
                    break;
                }

                var targetCharacterType = GetCharacterType();
                var targetCharacter = GetCharacter(targetCharacterType);
                if (targetCharacter != null)
                {
                    targetCharacter.isDestroyed = true;
                    cell = _contexts.game.GetCellWithPosition(targetCharacter.cellPosition.Value);
                    if (cell != null)
                    {
                        cell.isWalkable = true;
                    }
                }

                var active = targetCharacterType == 0;
                _contexts.game.CreateCharacter(position.x, position.y, active, targetCharacterType);
            }
        }

        private int GetCharacterType()
        {
            return _contexts.config.gameStateService.value.EditorMode == EditorModeType.FirstPlayer
                ? 0
                : 1;
        }

        private GameEntity GetCharacter(Vector2Int position)
        {
            foreach (var character in _characters)
            {
                if (character.cellPosition.Value == position)
                {
                    return character;
                }
            }
            
            return null;
        }
        
        private GameEntity GetCharacter(int type)
        {
            foreach (var character in _characters)
            {
                if (character.character.Type == type)
                {
                    return character;
                }
            }

            Debug.Log($"<color=red>Failed to find character with type {type}</color>");
            return null;
        }
    }
}