using Cameras;
using Characters;
using Entitas.CodeGeneration.Attributes;
using Grids;

namespace Game
{
    public class GameSceneArguments : IGameSceneArguments
    {
        public ICameraView CameraView { get; set; }
        public CellView GridCellPrefab { get; set; }
        public CharacterView CharacterView { get; set; }
    }

    [Config, Unique, ComponentName("GameSceneArguments")]
    public interface IGameSceneArguments
    {
        ICameraView CameraView { get; }
        CellView GridCellPrefab { get; }
        CharacterView CharacterView { get; }
    }
}