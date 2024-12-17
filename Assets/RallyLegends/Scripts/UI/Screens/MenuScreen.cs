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

        public event UnityAction PlayButtonClick;
        public event UnityAction GarageButtonClick;
        public event UnityAction SettingsButtonClick;
        public event UnityAction LeaderboardButtonClick;

        public void OnPlayButtonClick() => PlayButtonClick?.Invoke();

        public void OnGarageButtonClick() => GarageButtonClick?.Invoke();

        public void OnSettingsButtonClick() => SettingsButtonClick?.Invoke();

        public void OnLeaderboardButtonClick() => LeaderboardButtonClick?.Invoke();

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