using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Paths
{
    [Game]
    public class FollowPathComponent : IComponent
    {
        public List<Vector2Int> Value;
    }
}