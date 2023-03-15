using System;
using Entitas;

namespace Game
{
    public class GameController
    {
        private readonly Systems _systems;
        private readonly Systems _lockableSystems;
        private readonly Systems _cleanupSystems;
        private readonly Systems _editorsSystems;
        private readonly Contexts _contexts;

        public GameController(Contexts contexts, IGameSceneArguments gameSceneArgs, IGameSettings gameSettings)
        {
            _contexts = contexts;
            
            contexts.config.SetGameSettings(gameSettings);
            contexts.config.SetGameSceneArguments(gameSceneArgs);
            contexts.config.SetGameStateService(new GameStateService());
            
            
            _systems = new GameSystems(contexts);
            _lockableSystems = new LockableSystems(contexts);
            _cleanupSystems = new CleanupSystems(contexts);
            _editorsSystems = new EditorsSystems(contexts);
        }
        
        public void Initialize()
        {
            _systems.Initialize();
        }

        public void Execute()
        {
            _systems.Execute();

            if (!_contexts.config.gameStateService.value.IsInputLocked)
            {
                _lockableSystems.Execute();
            }

            if (EditorMode != EditorModeType.None)
            {
                _editorsSystems.Execute();
            }
            
            _cleanupSystems.Execute();
            _systems.Cleanup();
            _lockableSystems.Cleanup();
        }

        private static EditorModeType EditorMode => Contexts.sharedInstance.config.gameStateService.value.EditorMode;
    }
}