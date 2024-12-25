using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RallyLegends.UI
{
    public class MenuScreen : Screen
    {
        [SerializeField] private Button _playMenuButton;
        [SerializeField] private Button _gargeMenuButton;
        [SerializeField] private Button _settingsMenuButton;
        [SerializeField] private Button _leaderboardMenuButton;

        public event UnityAction PlayButtonClicked;
        public event UnityAction GarageButtonClicked;
        public event UnityAction SettingsButtonClicked;
        public event UnityAction LeaderboardButtonClicked;

        public void OnPlayButtonClick() => PlayButtonClicked?.Invoke();

        public void OnGarageButtonClick() => GarageButtonClicked?.Invoke();

        public void OnSettingsButtonClick() => SettingsButtonClicked?.Invoke();

        public void OnLeaderboardButtonClick() => LeaderboardButtonClicked?.Invoke();

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
            _playMenuButton.interactable = value;
            _playMenuButton.image.raycastTarget = value;

            _gargeMenuButton.interactable = value;
            _gargeMenuButton.image.raycastTarget = value;

            _settingsMenuButton.interactable = value;
            _settingsMenuButton.image.raycastTarget = value;

            _leaderboardMenuButton.interactable = value;
            _leaderboardMenuButton.image.raycastTarget = value;
        }
    }
}