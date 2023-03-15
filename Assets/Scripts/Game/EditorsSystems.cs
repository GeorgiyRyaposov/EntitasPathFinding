using Characters;

namespace Game
{
    public sealed class CharactersEditorsSystems : Feature
    {
        public CharactersEditorsSystems(Contexts contexts)
        {
            Add(new SetCharacterSystem(contexts));
        }
    }
}