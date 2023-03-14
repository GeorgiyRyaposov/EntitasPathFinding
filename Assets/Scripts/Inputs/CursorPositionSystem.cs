using System.Collections.Generic;
using Cameras;
using Entitas;
using Game;
using Grids;
using UnityEngine;

namespace Inputs
{
    public class CursorPositionSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;
        private readonly ICameraView _cameraView;
        private readonly IGameSettings _gameSettings;

        public CursorPositionSystem(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
            _cameraView = _contexts.config.gameSceneArguments.value.CameraView;
            _gameSettings = _contexts.config.gameSettings.value;
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher.CursorInput);

        protected override bool Filter(InputEntity entity) => entity.hasCursorInput;

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                var position = entity.cursorInput.value.Position;
                var e = _contexts.game.GetCellWithPosition(position);
                if (e != null)
                {
                    e.ReplaceGridsCellState((int) ECellState.Highlight);
                }
                
                entity.Destroy();
            }
        }
    }
}