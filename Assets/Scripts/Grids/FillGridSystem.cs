using Entitas;
using Game;
using UnityEngine;

namespace Grids
{
    public class FillGridSystem : IInitializeSystem
    {
        private readonly IGameSettings _gameSettings;
        private readonly IGameSceneArguments _gameSceneArgs;
        private readonly Contexts _contexts;

        public FillGridSystem(Contexts contexts)
        {
            _contexts = contexts;
            _gameSceneArgs = _contexts.config.gameSceneArguments.value;
            _gameSettings = _contexts.config.gameSettings.value;
        }
        
        public void Initialize()
        {
            var root = new GameObject("Root").transform;
            for (var x = 0; x < _gameSettings.GridSize.x; x++)
            {
                for (var y = 0; y < _gameSettings.GridSize.y; y++)
                {
                    CreateCell(x, y, root);
                }
            }
        }
        
        private void CreateCell(int x, int y, Transform root)
        {
            var entity = _contexts.game.CreateEntity();
            
            var cell = Object.Instantiate(_gameSceneArgs.GridCellPrefab, root);
            cell.transform.position = new Vector3(x, 0, y);
            cell.Link(entity);
            
            entity.AddGridsCell(true);
            entity.AddGridsCellPosition(new Vector2Int(x, y));
            entity.AddGridsCellState((int)ECellState.None);
        }
    }
}