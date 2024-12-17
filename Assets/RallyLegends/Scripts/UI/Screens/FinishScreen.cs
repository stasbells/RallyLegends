using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RallyLegends.GameLogic;

namespace RallyLegends.UI
{
    public class FinishScreen : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Image _winnerBoard;
        [SerializeField] private Image _loserBoard;
        [SerializeField] private LevelInstaller _level;
        [SerializeField] private SceneTransition _sceneTransition;

        private CanvasGroup _canvasGroup;

        public event UnityAction RestartButtonClick;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        private void OnEnable()
        {
            _level.Won += ShowWinnerBord;
            _level.Lost += ShowLoserBoard;
        }

        private void OnDisable()
        {
            _level.Won -= ShowWinnerBord;
            _level.Lost -= ShowLoserBoard;
        }

        public void OnRestartButtonClick()
        {
            RestartButtonClick?.Invoke();
            _sceneTransition.GoToMenu();
        }

        private void ShowWinnerBord()
        {
            SetInteractable(true);
            _winnerBoard.gameObject.SetActive(true);
        }

        private void ShowLoserBoard()
        {
            SetInteractable(true);
            _loserBoard.gameObject.SetActive(true);
        }

        private void SetInteractable(bool value)
        {
            _canvasGroup.alpha = 1f;

            _restartButton.interactable = value;
            _rewardButton.interactable = value;

            _restartButton.image.raycastTarget = value;
            _rewardButton.image.raycastTarget = value;
        }
    }
}