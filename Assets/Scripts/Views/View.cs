using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Views
{
    public class View : MonoBehaviour, IView, IDestroyedListener
    {
        protected GameEntity LinkedEntity;

        private bool _linked;

        public virtual void Link(IEntity entity)
        {
            gameObject.Link(entity);
            LinkedEntity = (GameEntity)entity;
            LinkedEntity.AddDestroyedListener(this);
            _linked = true;
        }

        public virtual void OnDestroyed(GameEntity entity) => OnDestroy();

        protected virtual void OnDestroy()
        {
            if (!_linked)
            {
                return;
            }
            _linked = false;
            
            gameObject.Unlink();
            Destroy(gameObject);
        }
    }
}