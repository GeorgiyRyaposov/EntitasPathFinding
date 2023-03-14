using Entitas;

namespace Game
{
    public class GameController
    {
        private readonly Systems _systems;
        private readonly Systems _lockableSystems;
        private readonly Contexts _contexts;

        public GameController(Contexts contexts, GameSceneArguments gameSceneArgs, IGameSettings gameSettings)
        {
            _contexts = contexts;
            
            contexts.config.SetGameSettings(gameSettings);
            contexts.config.SetGameSceneArguments(gameSceneArgs);
            contexts.config.SetGameStateService(new GameStateService());
            
            
            _systems = new GameSystems(contexts);
            _lockableSystems = new LockableSystems(contexts);
        }
        
        public void Initialize()
        {
            _systems.Initialize();
        }

        public void Execute()
        {
            _systems.Execute();
            _systems.Cleanup();

            if (!_contexts.config.gameStateService.value.IsInputLocked)
            {
                _lockableSystems.Execute();
            }
            _lockableSystems.Cleanup();
        }
    }
}