using DG.Tweening;
using Entitas;
using UnityEngine;
using Views;

namespace Characters
{
    public class CharacterView : View, ICellPositionListener
    {
        [SerializeField] private Material _redMaterial;
        [SerializeField] private Material _blueMaterial;
        [SerializeField] private MeshRenderer _renderer;

        private bool _startPositionSet;
        
        public override void Link(IEntity entity)
        {
            base.Link(entity);
            
            LinkedEntity.AddCellPositionListener(this);

            _renderer.material = LinkedEntity.character.Type == 0
                ? _redMaterial
                : _blueMaterial;
        }

        public void OnCellPosition(GameEntity entity, Vector2Int value)
        {
            var targetPos = new Vector3(value.x, 0, value.y);
            
            if (!_startPositionSet)
            {
                _startPositionSet = true;
                transform.position = targetPos;
                return;
            }
            
            var delay = Contexts.sharedInstance.config.gameSettings.value.PathStepDelay;
            transform.DOJump(targetPos, 0.2f, 1, delay);
        }
    }
}