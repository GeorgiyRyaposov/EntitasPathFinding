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
            var gridSize = _gameSettings.GridSize;
            var root = new GameObject("Root").transform;
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    CreateCell(x, y, root);
                }
            }

            CreateCharacter(0, 0, true, root);
            CreateCharacter(gridSize.x - 1, gridSize.y - 1, false, root);
        }
        
        private void CreateCell(int x, int y, Transform root)
        {
            var entity = _contexts.game.CreateEntity();
            
            var cell = Object.Instantiate(_gameSceneArgs.GridCellPrefab, root);
            cell.transform.position = new Vector3(x, 0, y);
            cell.Link(entity);
            
            entity.isGridsCell = true;
            entity.isGridsWalkable = true;
            entity.AddGridsCellPosition(new Vector2Int(x, y));
            entity.AddGridsCellState((int)ECellState.None);
        }
        
        private void CreateCharacter(int x, int y, bool active, Transform root)
        {
            var entity = _contexts.game.CreateEntity();
            
            var character = Object.Instantiate(_gameSceneArgs.CharacterView, root);
            character.transform.position = new Vector3(x, 0, y);
            character.Link(entity);
            
            entity.AddCharactersCharacter(active);
            entity.AddGridsCellPosition(new Vector2Int(x, y));

            var cell = _contexts.game.GetCellWithPosition(new Vector2Int(x, y));
            if (cell != null)
            {
                cell.isGridsWalkable = false;
            }
        }
    }
}