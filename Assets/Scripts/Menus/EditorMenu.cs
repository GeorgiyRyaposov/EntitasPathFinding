using System;
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

        private void SetFirstPlayerMode()
        {
            SetPlayerMode(true);
        }
        private void SetSecondPlayerMode()
        {
            SetPlayerMode(false);
        }
        private void SetPlayerMode(bool first)
        {
            
        }

        private void SetObstacleMode()
        {
            
        }
    }
}