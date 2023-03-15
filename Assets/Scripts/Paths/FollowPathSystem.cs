using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Utils;

namespace Paths
{
    public class FollowPathSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _characters;
        private readonly float _pathStepDelay;
        private readonly IGroup<GameEntity> _cells;

        public FollowPathSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
            
            _characters = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character));
            _pathStepDelay = contexts.config.gameSettings.value.PathStepDelay;
            _cells = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Cell, GameMatcher.CellPosition));
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.FollowPath);

        protected override bool Filter(GameEntity entity) => entity.hasFollowPath;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var character = GetActiveCharacter();
                CoroutineHelper.Run(FollowPath(character, entity.followPath.Value));

                entity.Destroy();
                break;
            }
        }

        private IEnumerator FollowPath(GameEntity character, List<Vector2Int> path)
        {
            LockInput(true);
            
            var cell = _contexts.game.GetCellWithPosition(path[0]);
            cell.isWalkable = true;
            
            cell = _contexts.game.GetCellWithPosition(path[path.Count - 1]);
            cell.isWalkable = false;
            
            var delay = new WaitForSeconds(_pathStepDelay);
            for (int i = 0; i < path.Count; i++)
            {
                var position = path[i];
                character.ReplaceCellPosition(position);
                yield return delay;
            }
            
            LockInput(false);
        }

        private void LockInput(bool lockInput)
        {
            _contexts.config.gameStateService.value.LockInput(lockInput);
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