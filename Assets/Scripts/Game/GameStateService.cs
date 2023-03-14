using Entitas.CodeGeneration.Attributes;

namespace Game
{
    public class GameStateService : IGameStateService
    {
        public bool IsInputLocked { get; set; }
    }

    [Config, Unique, ComponentName("GameStateService")]
    public interface IGameStateService
    {
        bool IsInputLocked { get; set; }
    }
}