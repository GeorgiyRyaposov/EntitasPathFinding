using Inputs;

namespace Game
{
    public class CleanUpSystems : Feature
    {
        public CleanUpSystems(Contexts contexts)
        {
            Add(new DestroyInputSystem(contexts));
        }
    }
}