using Entitas;
using UnityEngine;

namespace Paths
{
    [Game]
    public class FindPathRequest : IComponent
    {
        public Vector2Int Start;
        public Vector2Int Finish;
    }
}