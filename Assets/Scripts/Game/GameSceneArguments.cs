using Cameras;
using Entitas.CodeGeneration.Attributes;
using Grids;
using UnityEngine;

namespace Game
{
    public class GameSceneArguments : IGameSceneArguments
    {
        public ICameraView CameraView { get; set; }
        public CellView GridCellPrefab { get; set; }
    }

    [Config, Unique, ComponentName("GameSceneArguments")]
    public interface IGameSceneArguments
    {
        ICameraView CameraView { get; }
        CellView GridCellPrefab { get; }
    }
}