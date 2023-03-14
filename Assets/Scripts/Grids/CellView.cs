using Entitas;
using Entitas.Unity;
using UnityEngine;
using Views;

namespace Grids
{
    public class CellView : MonoBehaviour, IView, IGridsCellStateListener
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Material _errorMaterial;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _highlightMaterial;
        
        private GameEntity _linkedEntity;
        
        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            _linkedEntity = (GameEntity)entity;
            _linkedEntity.AddGridsCellStateListener(this);
        }

        public void OnGridsCellState(GameEntity entity, int value)
        {
            var state = (ECellState)value;
            UpdateState(state);
        }

        private void UpdateState(ECellState state)
        {
            _renderer.sharedMaterial = GetMaterial(state);
        }

        private Material GetMaterial(ECellState state)
        {
            return state switch
            {
                ECellState.None => _defaultMaterial,
                ECellState.Highlight => _highlightMaterial,
                ECellState.Error => _errorMaterial,
                _ => _defaultMaterial
            };
        }
    }
}