using Entitas;

namespace Game
{
    public class GameController
    {
        private readonly Systems _systems;

        public GameController(Contexts contexts, GameSceneArguments gameSceneArgs, IGameSettings gameSettings)
        {
            contexts.config.SetGameSettings(gameSettings);
            contexts.config.SetGameSceneArguments(gameSceneArgs);
            
            _systems = new GameSystems(contexts);
        }
        
        public void Initialize()
        {
            _systems.Initialize();
        }

        public void Execute()
        {
            _systems.Execute();
            _systems.Cleanup();
        }
    }
}