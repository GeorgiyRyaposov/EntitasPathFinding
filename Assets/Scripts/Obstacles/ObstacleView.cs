using Entitas;
using UnityEngine;
using Views;

namespace Obstacles
{
    public class ObstacleView : View, ICellPositionListener
    {
        public override void Link(IEntity entity)
        {
            base.Link(entity);
            
            LinkedEntity.AddCellPositionListener(this);
        }
        
        public void OnCellPosition(GameEntity entity, Vector2Int value)
        {
            transform.position = new Vector3(value.x, 0, value.y);
        }
    }
}