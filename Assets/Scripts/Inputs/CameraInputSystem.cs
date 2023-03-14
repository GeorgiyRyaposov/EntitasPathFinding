using System.Collections.Generic;
using Cameras;
using Entitas;
using Game;
using UnityEngine;

namespace Inputs
{
    public class CameraInputSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;
        private readonly ICameraView _cameraView;
        private readonly IGameSettings _gameSettings;

        public CameraInputSystem(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
            _cameraView = _contexts.config.gameSceneArguments.value.CameraView;
            _gameSettings = _contexts.config.gameSettings.value;
        }
        
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher.InputsCameraPositionInput);

        protected override bool Filter(InputEntity entity) => entity.hasInputsCameraPositionInput;

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                var value = Time.deltaTime * _gameSettings.CameraSens * entity.inputsCameraPositionInput.Value;
                _cameraView.MovePosition(new Vector3(value.x, 0, value.y));
                entity.Destroy();
            }
        }
    }
}