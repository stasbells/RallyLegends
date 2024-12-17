using UnityEngine;
using UnityEngine.Events;
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

        public event UnityAction GameStarted;

        private void OnEnable()
        {
            _menuScreen.PlayButtonClick += OnStartButtonClick;
            _menuScreen.GarageButtonClick += OnGarageButtonClick;
            _menuScreen.SettingsButtonClick += OnSettingsButtonClick;
            _menuScreen.LeaderboardButtonClick += OnLeaderbordButtonClick;
            _startScreen.StartButtonClick += OnPlayButtonClick;
            _startScreen.BackToMenuButtonClick += OnMenuButtonClick;
            _garageScreen.BackToMenuButtonClick += OnMenuButtonClick;
            _settingsScreen.BackToMenuButtonClick += OnMenuButtonClick;
            _leaderboardScreen.BackToMenuButtonClick += OnMenuButtonClick;
        }

        private void OnDisable()
        {
            _menuScreen.PlayButtonClick -= OnStartButtonClick;
            _menuScreen.GarageButtonClick -= OnGarageButtonClick;
            _menuScreen.SettingsButtonClick -= OnSettingsButtonClick;
            _menuScreen.LeaderboardButtonClick -= OnLeaderbordButtonClick;
            _startScreen.StartButtonClick -= OnPlayButtonClick;
            _startScreen.BackToMenuButtonClick -= OnMenuButtonClick;
            _garageScreen.BackToMenuButtonClick -= OnMenuButtonClick;
            _settingsScreen.BackToMenuButtonClick -= OnMenuButtonClick;
            _leaderboardScreen.BackToMenuButtonClick -= OnMenuButtonClick;
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

        private void OnStartGame()
        {
            GameStarted?.Invoke();
            _sceneTransition.GoToGame();
        }
    }
}