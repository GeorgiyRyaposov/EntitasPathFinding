using Common;
using Inputs;

namespace Game
{
    public sealed class CleanupSystems : Feature
    {
        public CleanupSystems(Contexts contexts)
        {
            Add(new DestroyInputSystem(contexts));
            Add(new DestroyedGameSystem(contexts));
        }
    }
}