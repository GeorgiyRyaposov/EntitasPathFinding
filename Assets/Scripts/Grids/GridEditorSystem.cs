using System.Collections.Generic;
using Characters;
using Entitas;
using Game;
using UnityEngine;

namespace Grids
{
    public class GridEditorSystem : ReactiveSystem<InputEntity>
    {
        private EditorModeType EditorMode => _contexts.config.gameStateService.value.EditorMode;
        
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _characters;
        private readonly IGroup<GameEntity> _obstacles;
        

        public GridEditorSystem(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
            _characters = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character));
            _obstacles = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Obstacle));
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        { 
            return context.CreateCollector(InputMatcher.CursorInput);
        }

        protected override bool Filter(InputEntity entity) => 
            entity.hasCursorInput && entity.cursorInput.value.Pressed && !entity.cursorInput.value.OverUI;

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
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

                switch (EditorMode)
                {
                    case EditorModeType.Obstacle:
                        AddOrRemoveObstacle(position);
                        break;
                    
                    case EditorModeType.FirstPlayer:
                    case EditorModeType.SecondPlayer:
                        AddOrRemovePlayer(position);
                        break;
                }
            }
        }

        private void AddOrRemovePlayer(Vector2Int position)
        {
            var obstacle = GetObstacle(position);
            if (obstacle != null)
            {
                return;
            }
            
            var targetCharacterType = GetCharacterType();
            var targetCharacter = GetCharacter(targetCharacterType);
            if (targetCharacter != null)
            {
                targetCharacter.isDestroyed = true;
                var cell = _contexts.game.GetCellWithPosition(targetCharacter.cellPosition.Value);
                if (cell != null)
                {
                    cell.isWalkable = true;
                }
            }

            var active = targetCharacterType == 0;
            _contexts.game.CreateCharacter(position.x, position.y, active, targetCharacterType);
        }
        
        

        private int GetCharacterType()
        {
            return EditorMode == EditorModeType.FirstPlayer
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

        private void AddOrRemoveObstacle(Vector2Int position)
        {
            var cell = _contexts.game.GetCellWithPosition(position);
            var obstacle = GetObstacle(position);
            if (obstacle != null)
            {
                obstacle.isDestroyed = true;
                cell.isWalkable = true;
            }
            else
            {
                CreateObstacle(position);
                cell.isWalkable = false;
            }
        }
        
        
        private GameEntity GetObstacle(Vector2Int position)
        {
            foreach (var obstacle in _obstacles)
            {
                if (obstacle.cellPosition.Value == position)
                {
                    return obstacle;
                }
            }
            
            return null;
        }
        
        private void CreateObstacle(Vector2Int position)
        {
            var entity = _contexts.game.CreateEntity();
            entity.isObstacle = true;
            entity.AddCellPosition(position);
            entity.AddAsset("ObstacleView");
        }
    }
}