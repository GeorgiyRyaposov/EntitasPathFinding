using System;
using UnityEngine;

namespace Menus
{
    public class MenusManager : MonoBehaviour
    {
        [SerializeField] private EditorMenu _editorMenu;
        [SerializeField] private GameMenu _gameMenu;

        [SerializeField] private CustomToggle _editorToggle;
        [SerializeField] private CustomToggle _gameToggle;

        [SerializeField] private CanvasGroup _rootCanvasGroup;
        
        private void Start()
        {
            _editorToggle.SetOnClick(OnEditorToggle);
            _gameToggle.SetOnClick(OnGameToggle);

            _editorToggle.SetToggleOn();
        }

        private void OnEditorToggle()
        {
            if (Contexts.sharedInstance.config.gameStateService.value.IsInputLocked ||
                Contexts.sharedInstance.config.gameStateService.value.IsUILocked)
            {
                return;
            }
            
            _editorMenu.Show();
            _gameMenu.Hide();
        }
        private void OnGameToggle()
        {
            if (Contexts.sharedInstance.config.gameStateService.value.IsUILocked)
            {
                return;
            }
            
            _editorMenu.Hide();
            _gameMenu.Show();
        }

        private void Update()
        {
            _rootCanvasGroup.interactable = !Contexts.sharedInstance.config.gameStateService.value.IsUILocked;
        }
    }
}