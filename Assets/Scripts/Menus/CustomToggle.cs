using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menus
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        
        private UnityAction _callback;
        private bool _isSelected;

        private static CustomToggle _activeToggle;
        
        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
            ResetState();
        }

        public void SetOnClick(UnityAction call)
        {
            _callback = call;
        }

        public void ResetState()
        {
            _isSelected = false;
            UpdateVisualState();
        }

        public void SetToggleOn()
        {
            if (!_isSelected)
            {
                OnButtonClick();
            }
        }
        

        private void OnButtonClick()
        {
            _isSelected = !_isSelected;
            UpdateVisualState();
            
            _callback.Invoke();

            if (_isSelected)
            {
                if (_activeToggle != null)
                {
                    _activeToggle.ResetState();
                }

                _activeToggle = this;
            }
        }

        private void UpdateVisualState()
        {
            var color = _isSelected
                ? Color.grey
                : Color.white;

            _background.DOColor(color, 0.2f).SetEase(Ease.Flash);
        }
    }
}