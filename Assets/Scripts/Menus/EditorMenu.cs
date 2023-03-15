using Game;
using UnityEngine;

namespace Menus
{
    public class EditorMenu : MonoBehaviour
    {
        [SerializeField] private CustomToggle _firstPlayerModeToggle;
        [SerializeField] private CustomToggle _secondPlayerModeToggle;
        [SerializeField] private CustomToggle _obstaclesModeToggle;

        private void Start()
        {
            _firstPlayerModeToggle.SetOnClick(SetFirstPlayerMode);
            _secondPlayerModeToggle.SetOnClick(SetSecondPlayerMode);
            _obstaclesModeToggle.SetOnClick(SetObstacleMode);
        }

        public void Show()
        {
            SetEditorMode(EditorModeType.None);
            
            _firstPlayerModeToggle.ResetState();
            _secondPlayerModeToggle.ResetState();
            _obstaclesModeToggle.ResetState();
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            SetEditorMode(EditorModeType.None);
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
    }
}