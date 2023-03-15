using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Button _endTurnButton;

        private IGroup<GameEntity> _characters;
        
        private void Start()
        {
            _endTurnButton.onClick.AddListener(OnEndTurn);
            _characters = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.Character));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEndTurn()
        {
            if (Contexts.sharedInstance.config.gameStateService.value.IsInputLocked)
            {
                return;
            }
        
            foreach (var character in _characters)
            {
                character.character.Active = !character.character.Active;
            }
        }
    }
}