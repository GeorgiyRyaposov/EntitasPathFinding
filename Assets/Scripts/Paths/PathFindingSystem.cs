using System.Collections.Generic;
using Entitas;
using Grids;
using UnityEngine;

namespace Paths
{
    public class PathFindingSystem : ReactiveSystem<GameEntity>
    {
        private const int MOVE_STRAIGHT_COST = 10;
        
        private readonly Contexts _contexts;
        private readonly PathNode[] _pathNodes;
        private readonly Vector2Int _gridSize;
        private readonly IGroup<GameEntity> _cells;

        public PathFindingSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
            
            _gridSize = _contexts.config.gameSettings.value.GridSize;

            _pathNodes = new PathNode[_gridSize.x * _gridSize.y];
            _cells = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.GridsCell, GameMatcher.GridsCellPosition));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.PathsFindPathRequest);

        protected override bool Filter(GameEntity entity) => entity.hasPathsFindPathRequest;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var request = entity.pathsFindPathRequest;
                var path = FindPath(request.Start, request.Finish);

                ClearCellsState();

                HighlightPath(path);

                if (request.FollowPath)
                {
                    var followPath = _contexts.game.CreateEntity();
                    followPath.AddPathsFollowPath(path);
                }
                
                entity.Destroy();
            }
        }

        private void HighlightPath(List<Vector2Int> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var cell = _contexts.game.GetCellWithPosition(path[i]);
                if (cell != null)
                {
                    cell.ReplaceGridsCellState((int) ECellState.Highlight);
                }
            }
        }

        private void ClearCellsState()
        {
            foreach (var cell in _cells)
            {
                cell.ReplaceGridsCellState(0);
            }
        }

        private readonly List<int> _openList = new List<int>();

        private readonly Vector2Int[] _neighboursIndexes = new[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
        };
        private List<Vector2Int> FindPath(Vector2Int start, Vector2Int finish)
        {
            //reset values
            foreach (var cell in _cells)
            {
                var x = cell.gridsCellPosition.Value.x;
                var y = cell.gridsCellPosition.Value.y;
                var index = SquareGridCellUtil.CalculateIndex(x, y, _gridSize.x);
                var node = new PathNode()
                {
                    X = x,
                    Y = y,
                    Index = index,
                    GCost = int.MaxValue,
                    HCost = CalculateDistance(new Vector2Int(x, y), finish),
                    IsWalkable = cell.isGridsWalkable,
                    CameFromIndex = -1,
                };
                node.CalculateFCost();
                
                _pathNodes[index] = node;
            }
            
            var endNodeIndex = SquareGridCellUtil.CalculateIndex(finish.x, finish.y, _gridSize.x);
            var startNode = _pathNodes[SquareGridCellUtil.CalculateIndex(start.x, start.y, _gridSize.x)];

            startNode.GCost = 0;
            startNode.HCost = CalculateDistance(start, finish);
            startNode.CalculateFCost();

            _pathNodes[startNode.Index] = startNode;

            _openList.Clear();
            //var closedList = new NativeList<int>(Allocator.Temp);
            _openList.Add(startNode.Index);

            while (_openList.Count > 0)
            {
                var currentNodeIndex = GetLowestFCostNode(_openList, _pathNodes);
                if (currentNodeIndex == endNodeIndex)
                {
                    break;
                }

                for (int i = 0; i < _openList.Count; i++)
                {
                    if (_openList[i] == currentNodeIndex)
                    {
                        _openList.RemoveAt(i);
                        break;
                    }
                }

                //closedList.Add(currentNodeIndex);

                var currentNode = _pathNodes[currentNodeIndex];
                for (var i = 0; i < _neighboursIndexes.Length; i++)
                {
                    var neighborIndex = _neighboursIndexes[i];
                    var neighbourCell = new Vector2Int(currentNode.X + neighborIndex.x, currentNode.Y + neighborIndex.y);
                    if (!IsInsideArray(neighbourCell, _gridSize))
                    {
                        continue;
                    }

                    var neighbourIndex = SquareGridCellUtil.CalculateIndex(neighbourCell.x, neighbourCell.y, _gridSize.x);
                    // if (closedList.Contains(neighbourIndex))
                    // {
                    //     //already searched
                    //     continue;
                    // }

                    if (!_pathNodes[neighbourIndex].IsWalkable)
                    {
                        //closedList.Add(neighbourIndex);
                        continue;
                    }

                    var neighbourNode = _pathNodes[neighbourIndex];
                    var currentNodeCell = new Vector2Int(currentNode.X, currentNode.Y);
                    var tentativeGCost = currentNode.GCost + CalculateDistance(currentNodeCell, neighbourCell);
                    if (tentativeGCost >= neighbourNode.GCost)
                    {
                        continue;
                    }

                    neighbourNode.CameFromIndex = currentNodeIndex;
                    neighbourNode.GCost = tentativeGCost;
                    neighbourNode.CalculateFCost();
                    _pathNodes[neighbourNode.Index] = neighbourNode;

                    if (!_openList.Contains(neighbourNode.Index))
                    {
                        _openList.Add(neighbourNode.Index);
                    }
                }
            } //end of while loop
            
            var endNode = _pathNodes[endNodeIndex];
            return CalculatePath(endNode, _pathNodes);
        }
        
        private List<Vector2Int> CalculatePath(PathNode endNode, PathNode[] nodes)
        {
            var path = new List<Vector2Int>();
            if (endNode.CameFromIndex == -1)
            {
                //failed to find path
                return path;
            }

            path.Add(endNode.CellIndex);
            var currentNode = endNode;

            while (currentNode.CameFromIndex != -1)
            {
                var cameFromNode = nodes[currentNode.CameFromIndex];
                path.Add(cameFromNode.CellIndex);
                currentNode = cameFromNode;
            }

            path.Reverse();
            
            return path;
        }
        
        private int CalculateDistance(Vector2Int a, Vector2Int b)
        {
            var xDist = Mathf.Abs(a.x - b.x);
            var yDist = Mathf.Abs(a.y - b.y);
            var remaining = Mathf.Abs(xDist - yDist);
            return MOVE_STRAIGHT_COST * remaining;
        }
        
        private bool IsInsideArray(Vector2Int index, Vector2Int gridSize)
        {
            return index.x >= 0 && index.y >= 0 && index.x < gridSize.x && index.y < gridSize.y;
        }
        
        private int GetLowestFCostNode(IReadOnlyList<int> openList, IReadOnlyList<PathNode> nodes)
        {
            var lowestNode = nodes[openList[0]];
            for (int i = 1; i < openList.Count; i++)
            {
                var checkNode = nodes[openList[i]];
                if (checkNode.FCost < lowestNode.FCost)
                {
                    lowestNode = checkNode;
                }
            }

            return lowestNode.Index;
        }
    }
}