using UnityEngine;

namespace Menus
{
    public class MenusManager : MonoBehaviour
    {
        [SerializeField] private EditorMenu _editorMenu;
        [SerializeField] private GameMenu _gameMenu;

        [SerializeField] private CustomToggle _editorToggle;
        [SerializeField] private CustomToggle _gameToggle;

        private void Start()
        {
            _editorToggle.SetOnClick(OnEditorToggle);
            _gameToggle.SetOnClick(OnGameToggle);

            _editorToggle.SetToggleOn();
        }

        private void OnEditorToggle()
        {
            _editorMenu.Show();
            _gameMenu.Hide();
        }
        private void OnGameToggle()
        {
            _editorMenu.Hide();
            _gameMenu.Show();
        }
    }
}