using Entitas.CodeGeneration.Attributes;

namespace Game
{
    public class GameStateService : IGameStateService
    {
        public bool IsInputLocked { get; private set; }
        public void LockInput(bool lockUI)
        {
            IsInputLocked = lockUI;
        }
        
        public bool IsUILocked { get; private set; }
        public void LockUI(bool lockUI)
        {
            IsUILocked = lockUI;
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
        void LockInput(bool lockUI);
        
        bool IsUILocked { get; }
        void LockUI(bool lockUI);
        
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