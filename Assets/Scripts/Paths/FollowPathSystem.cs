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

        public FollowPathSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
            
            _characters = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CharactersCharacter));
            _pathStepDelay = contexts.config.gameSettings.value.PathStepDelay;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.PathsFollowPath);

        protected override bool Filter(GameEntity entity) => entity.hasPathsFollowPath;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var character = GetActiveCharacter();
                CoroutineHelper.Run(FollowPath(character, entity.pathsFollowPath.Value));

                entity.Destroy();
                break;
            }
        }

        private IEnumerator FollowPath(GameEntity character, List<Vector2Int> path)
        {
            LockInput(true);
            
            var delay = new WaitForSeconds(_pathStepDelay);
            for (int i = 0; i < path.Count; i++)
            {
                var position = path[i];
                character.ReplaceGridsCellPosition(position);
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
                if (character.charactersCharacter.Active)
                {
                    return character;
                }
            }

            Debug.LogError($"<color=red>Failed to find active character</color>");
            return null;
        }
    }
}