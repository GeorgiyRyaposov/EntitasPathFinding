using Entitas;

namespace Characters
{
    [Game]
    public class CharacterComponent : IComponent
    {
        public bool Active;
        public int Type;
    }
}