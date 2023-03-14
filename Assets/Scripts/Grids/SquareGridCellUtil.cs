using UnityEngine;

namespace Grids
{
    public static class SquareGridCellUtil
    {
        public static Vector2Int GetCellIndex(Vector3 worldPos, Vector3 gridOriginPos, float gridCellSize)
        {
            var pos = worldPos - gridOriginPos;
            return new Vector2Int(Mathf.RoundToInt(pos.x / gridCellSize),
                                  Mathf.RoundToInt(pos.z / gridCellSize));
        }
        
        public static int CalculateIndex(int x, int y, int gridWidth)
        {
            return x + y * gridWidth;
        }
    }
}