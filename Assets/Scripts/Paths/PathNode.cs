using UnityEngine;

namespace Paths
{
    public struct PathNode
    {
        public int X;
        public int Y;
        public Vector2Int CellIndex => new Vector2Int(X, Y);
        
        public int Index;
        
        public int CurrentCost;
        public int ShortestCost;
        public int FinalCost;

        public int CameFromIndex;

        public bool IsWalkable;

        public void CalculateFinalCost()
        {
            FinalCost = CurrentCost + ShortestCost;
        }
    }
}