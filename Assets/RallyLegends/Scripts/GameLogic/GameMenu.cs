using UnityEngine;
using RallyLegends.UI;

namespace RallyLegends.GameLogic
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private StartScreen _startScreen;
        [SerializeField] private GarageScreen _garageScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private SceneTransition _sceneTransition;

        private void OnEnable()
        {
            _menuScreen.PlayButtonClicked += OnStartButtonClick;
            _menuScreen.GarageButtonClicked += OnGarageButtonClick;
            _menuScreen.SettingsButtonClicked += OnSettingsButtonClick;
            _menuScreen.LeaderboardButtonClicked += OnLeaderbordButtonClick;
            _startScreen.StartButtonClicked += OnPlayButtonClick;
            _startScreen.BackToMenuButtonClicked += OnMenuButtonClick;
            _garageScreen.BackToMenuButtonClicked += OnMenuButtonClick;
            _settingsScreen.BackToMenuButtonClicked += OnMenuButtonClick;
            _leaderboardScreen.BackToMenuButtonClicked += OnMenuButtonClick;
        }

        private void OnDisable()
        {
            _menuScreen.PlayButtonClicked -= OnStartButtonClick;
            _menuScreen.GarageButtonClicked -= OnGarageButtonClick;
            _menuScreen.SettingsButtonClicked -= OnSettingsButtonClick;
            _menuScreen.LeaderboardButtonClicked -= OnLeaderbordButtonClick;
            _startScreen.StartButtonClicked -= OnPlayButtonClick;
            _startScreen.BackToMenuButtonClicked -= OnMenuButtonClick;
            _garageScreen.BackToMenuButtonClicked -= OnMenuButtonClick;
            _settingsScreen.BackToMenuButtonClicked -= OnMenuButtonClick;
            _leaderboardScreen.BackToMenuButtonClicked -= OnMenuButtonClick;
        }

        private void Start()
        {
            _menuScreen.Open();
            _startScreen.Close();
            _garageScreen.Close();
            _leaderboardScreen.Close();
            _settingsScreen.Close();
        }

        private void OnStartButtonClick()
        {
            _menuScreen.Close();
            _startScreen.Open();
        }

        private void OnMenuButtonClick()
        {
            if (_garageScreen.GetComponent<CanvasGroup>().alpha == 1f)
                _garageScreen.Close();

            if (_startScreen.GetComponent<CanvasGroup>().alpha == 1f)
                _startScreen.Close();

            _settingsScreen.Close();
            _leaderboardScreen.Close();
            _menuScreen.Open();
        }

        private void OnGarageButtonClick()
        {
            _menuScreen.Close();
            _garageScreen.Open();
        }

        private void OnSettingsButtonClick()
        {
            _menuScreen.Close();
            _settingsScreen.Open();
        }

        private void OnLeaderbordButtonClick()
        {
            _menuScreen.Close();
            _leaderboardScreen.Open();
        }

        private void OnPlayButtonClick()
        {
            _startScreen.Close();
            OnStartGame();
        }

        private void OnStartGame() => _sceneTransition.GoToGame();
    }
}