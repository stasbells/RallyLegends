using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RallyLegends.UI
{
    public class SettingsScreen : Screen
    {
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _soundSlider;

        public event UnityAction BackToMenuButtonClick;

        public void OnBackToMenuButtonClick() => BackToMenuButtonClick?.Invoke();

        public override void Open()
        {
            CanvasGroup.alpha = 1f;
            SetInteractable(true);
        }

        public override void Close()
        {
            CanvasGroup.alpha = 0f;
            SetInteractable(false);
        }

        protected override void SetInteractable(bool value)
        {
            _backToMenuButton.interactable = value;
            _backToMenuButton.image.raycastTarget = value;

            _volumeSlider.gameObject.SetActive(value);
            _soundSlider.gameObject.SetActive(value);
        }
    }
}