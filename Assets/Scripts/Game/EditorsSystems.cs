using Grids;

namespace Game
{
    public sealed class EditorsSystems : Feature
    {
        public EditorsSystems(Contexts contexts)
        {
            Add(new GridEditorSystem(contexts));
        }
    }
}