using UnityEngine;

namespace Characters
{
    public static class CharacterContextExtension
    {
        public static void CreateCharacter(this GameContext context, int x, int y, bool active)
        {
            var entity = context.CreateEntity();
            entity.AddCharacter(active, active ? 0 : 1);
            entity.AddCellPosition(new Vector2Int(x, y));
            entity.AddAsset("CharacterView");
            
            var cell = context.GetCellWithPosition(new Vector2Int(x, y));
            if (cell != null)
            {
                cell.isWalkable = false;
            }
        }
    }
}