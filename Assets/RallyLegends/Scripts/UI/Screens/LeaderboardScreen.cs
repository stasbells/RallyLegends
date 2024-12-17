using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace RallyLegends.UI
{
    public class LeaderboardScreen : Screen
    {
        [SerializeField] private GameObject _popup;

        [Header("Leaderboards")]
        [SerializeField] private LeaderboardYG _forestLeaderboard;
        [SerializeField] private LeaderboardYG _winterLeaderboard;
        [SerializeField] private LeaderboardYG _swampLeaderboard;

        [Header("Buttons")]
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _backToLeaderboardScreenButton;
        [SerializeField] private Button _forestLiaderbaoardOpenButton;
        [SerializeField] private Button _winterLiaderboardOpenButton;
        [SerializeField] private Button _swampLiaderboardOpenButton;

        public event UnityAction BackToMenuButtonClick;

        public void OnMenuButtonClick() => BackToMenuButtonClick?.Invoke();

        public void SetActiveForestLiaderbord(bool value) => SetActive(_forestLeaderboard, value);

        public void SetActiveWinterLiaderbord(bool value) => SetActive(_winterLeaderboard, value);

        public void SetActiveSwampLiaderboard(bool value) => SetActive(_swampLeaderboard, value);

        public void SetActive(LeaderboardYG leaderboard, bool value)
        {
            leaderboard.gameObject.SetActive(value);
            _backToLeaderboardScreenButton.gameObject.SetActive(value);
            _backToLeaderboardScreenButton.gameObject.SetActive(value);
            _popup.SetActive(value);
            SetInteractable(!value);
        }

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
            _backToMenuButton.gameObject.SetActive(value);

            _forestLiaderbaoardOpenButton.interactable = value;
            _forestLiaderbaoardOpenButton.image.raycastTarget = value;
            _forestLiaderbaoardOpenButton.gameObject.SetActive(value);

            _winterLiaderboardOpenButton.interactable = value;
            _winterLiaderboardOpenButton.image.raycastTarget = value;
            _winterLiaderboardOpenButton.gameObject.SetActive(value);

            _swampLiaderboardOpenButton.interactable = value;
            _swampLiaderboardOpenButton.image.raycastTarget = value;
            _swampLiaderboardOpenButton.gameObject.SetActive(value);
        }
    }
}