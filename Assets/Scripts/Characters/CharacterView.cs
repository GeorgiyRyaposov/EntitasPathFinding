using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using Views;

namespace Characters
{
    public class CharacterView : MonoBehaviour, IView, IGridsCellPositionListener
    {
        private GameEntity _linkedEntity;
        
        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            _linkedEntity = (GameEntity)entity;
            _linkedEntity.AddGridsCellPositionListener(this);
        }

        public void OnGridsCellPosition(GameEntity entity, Vector2Int value)
        {
            var delay = Contexts.sharedInstance.config.gameSettings.value.PathStepDelay;
            var targetPos = new Vector3(value.x, 0, value.y);
            transform.DOJump(targetPos, 0.2f, 1, delay);
        }
    }
}