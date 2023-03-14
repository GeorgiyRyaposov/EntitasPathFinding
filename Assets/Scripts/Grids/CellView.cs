using Entitas;
using Entitas.Unity;
using UnityEngine;
using Views;

namespace Grids
{
    public class CellView : MonoBehaviour, IView
    {
        private GameEntity _linkedEntity;
        
        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            _linkedEntity = (GameEntity)entity;
            //_linkedEntity.AddGridsCellPositionListener(this);
        }
    }
}