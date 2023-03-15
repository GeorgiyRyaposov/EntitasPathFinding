using Characters;
using Paths;

namespace Game
{
    public sealed class LockableSystems : Feature
    {
        public LockableSystems(Contexts contexts)
        {
            Add(new ActiveCharacterSystem(contexts));
            Add(new PathFindingSystem(contexts));
            Add(new FollowPathSystem(contexts));
        }
    }
}