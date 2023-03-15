using Entitas.CodeGeneration.Attributes;

namespace Game
{
    public class GameStateService : IGameStateService
    {
        public bool IsInputLocked { get; private set; } = true;
        public void LockInput(bool lockInput)
        {
            IsInputLocked = lockInput;
        }
        
        
        public EditorModeType EditorMode { get; private set; }
        public void SetEditorMode(EditorModeType editorMode)
        {
            EditorMode = editorMode;
        }
    }

    [Config, Unique, ComponentName("GameStateService")]
    public interface IGameStateService
    {
        bool IsInputLocked { get; }
        void LockInput(bool lockInput);
        
        EditorModeType EditorMode { get; }
        void SetEditorMode(EditorModeType editorMode);
    }

    public enum EditorModeType
    {
        None,
        FirstPlayer,
        SecondPlayer,
        Obstacle
    }
}