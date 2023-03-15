using Entitas;
using Game;
using UnityEngine;

namespace Menus
{
    public class EditorMenu : MonoBehaviour
    {
        [SerializeField] private CustomToggle _firstPlayerModeToggle;
        [SerializeField] private CustomToggle _secondPlayerModeToggle;
        [SerializeField] private CustomToggle _obstaclesModeToggle;

        private IGroup<GameEntity> _cells;
        
        private void Start()
        {
            _firstPlayerModeToggle.SetOnClick(SetFirstPlayerMode);
            _secondPlayerModeToggle.SetOnClick(SetSecondPlayerMode);
            _obstaclesModeToggle.SetOnClick(SetObstacleMode);
            
            _cells = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.Cell, GameMatcher.CellPosition));
        }

        public void Show()
        {
            SetEditorMode(EditorModeType.None);
            LockInput(true);
            
            _firstPlayerModeToggle.ResetState();
            _secondPlayerModeToggle.ResetState();
            _obstaclesModeToggle.ResetState();
            
            gameObject.SetActive(true);
            
            ClearCellsState();
        }

        public void Hide()
        {
            SetEditorMode(EditorModeType.None);
            LockInput(false);
            gameObject.SetActive(false);
        }
        
        private void SetFirstPlayerMode()
        {
            SetEditorMode(EditorModeType.FirstPlayer);
        }
        private void SetSecondPlayerMode()
        {
            SetEditorMode(EditorModeType.SecondPlayer);
        }
        private void SetObstacleMode()
        {
            SetEditorMode(EditorModeType.Obstacle);
        }

        private void SetEditorMode(EditorModeType mode)
        {
            Contexts.sharedInstance.config.gameStateService.value.SetEditorMode(mode);
        }

        private void LockInput(bool lockInput)
        {
            Contexts.sharedInstance.config.gameStateService.value.LockInput(lockInput);
        }
        
        private void ClearCellsState()
        {
            if (_cells == null)
            {
                return;
            }
            
            foreach (var cell in _cells)
            {
                cell.ReplaceCellState(0);
            }
        }
    }
}