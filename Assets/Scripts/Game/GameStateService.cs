using Entitas.CodeGeneration.Attributes;

namespace Game
{
    public class GameStateService : IGameStateService
    {
        public bool IsInputLocked { get; private set; }
        public void LockInput(bool lockInput)
        {
            IsInputLocked = lockInput;
        }
    }

    [Config, Unique, ComponentName("GameStateService")]
    public interface IGameStateService
    {
        bool IsInputLocked { get; }
        void LockInput(bool lockInput);
    }
}