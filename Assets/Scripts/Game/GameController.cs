using System;
using Entitas;

namespace Game
{
    public class GameController
    {
        private readonly Systems _systems;
        private readonly Systems _lockableSystems;
        private readonly Systems _cleanupSystems;
        private readonly Systems _charactersEditorsSystems;
        private readonly Contexts _contexts;

        public GameController(Contexts contexts, GameSceneArguments gameSceneArgs, IGameSettings gameSettings)
        {
            _contexts = contexts;
            
            contexts.config.SetGameSettings(gameSettings);
            contexts.config.SetGameSceneArguments(gameSceneArgs);
            contexts.config.SetGameStateService(new GameStateService());
            
            
            _systems = new GameSystems(contexts);
            _lockableSystems = new LockableSystems(contexts);
            _cleanupSystems = new CleanupSystems(contexts);
            _charactersEditorsSystems = new CharactersEditorsSystems(contexts);
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

            switch (Contexts.sharedInstance.config.gameStateService.value.EditorMode)
            {
                case EditorModeType.None:
                    break;
                    
                case EditorModeType.FirstPlayer:
                case EditorModeType.SecondPlayer:
                    _charactersEditorsSystems.Execute();
                    break;
                    
                case EditorModeType.Obstacle:
                    break;
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _cleanupSystems.Execute();
            _systems.Cleanup();
            _lockableSystems.Cleanup();
            //_cleanupSystems.Cleanup();
        }
    }
}