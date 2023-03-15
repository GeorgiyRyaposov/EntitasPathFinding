using System.Collections.Generic;
using Cameras;
using Entitas;
using Game;
using UnityEngine;

namespace Inputs
{
    public class CameraInputSystem : ReactiveSystem<InputEntity>
    {
        private readonly ICameraView _cameraView;
        private readonly IGameSettings _gameSettings;

        public CameraInputSystem(Contexts contexts) : base(contexts.input)
        {
            _cameraView = contexts.config.gameSceneArguments.value.CameraView;
            _gameSettings = contexts.config.gameSettings.value;
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher.CameraPositionInput);

        protected override bool Filter(InputEntity entity) => entity.hasCameraPositionInput;

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                var value = Time.deltaTime * _gameSettings.CameraSens * entity.cameraPositionInput.Value;
                _cameraView.MovePosition(value);
                entity.Destroy();
            }
        }
    }
}