using UnityEngine;

namespace Grids
{
    public static class SquareGridCellUtil
    {
        public static int CalculateIndex(int x, int y, int gridWidth)
        {
            return x + y * gridWidth;
        }
    }
}