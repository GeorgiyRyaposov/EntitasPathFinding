using UnityEngine;

namespace Paths
{
    public struct PathNode
    {
        public int X;
        public int Y;
        public Vector2Int CellIndex => new Vector2Int(X, Y);
        
        public int Index;
        
        public int GCost; //current path cost
        public int HCost; //shortest path cost
        public int FCost; //final path cost

        public int CameFromIndex;

        public bool IsWalkable;

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }
    }
}