using Grids;
using Inputs;

namespace Game
{
    public sealed class GameSystems : Feature
    {
        public GameSystems(Contexts contexts)
        {
            // Input
            Add(new InputSystem(contexts));
            Add(new CameraInputSystem(contexts));
            
            // Events
            Add(new GameEventSystems(contexts));
            
            Add(new FillGridSystem(contexts));
            Add(new CursorPositionSystem(contexts));
        }
    }
}