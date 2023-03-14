using Entitas;
using Grids;
using UnityEngine;

public partial class Contexts
{
    public const string Cell = "Cell";

    [Entitas.CodeGeneration.Attributes.PostConstructor]
    public void InitializeCellEntityIndices()
    {
        game.AddEntityIndex(new PrimaryEntityIndex<GameEntity, Vector2Int>(
            Cell,
            game.GetGroup(GameMatcher
                .AllOf(GameMatcher.GridsCell, GameMatcher.GridsCellPosition)),
            (e, c) => (c as CellPosition)?.Value ?? e.gridsCellPosition.Value));
    }
}

public static class CellContextsExtension
{
    public static GameEntity GetCellWithPosition(this GameContext context, Vector2Int value) =>
        ((PrimaryEntityIndex<GameEntity, Vector2Int>)context.GetEntityIndex(Contexts.Cell)).GetEntity(value);
}