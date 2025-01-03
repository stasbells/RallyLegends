using UnityEngine;
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

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        private void OnEnable()
        {
            _level.LoserFinished += OnFinishShowWinnerBoard;
            _level.WinnerFinished += OnFinishShowLoserBoard;
        }

        private void OnDisable()
        {
            _level.LoserFinished -= OnFinishShowWinnerBoard;
            _level.WinnerFinished -= OnFinishShowLoserBoard;
        }

        public void OnRestartButtonClick()
        {
            _sceneTransition.GoToMenu();
        }

        private void OnFinishShowWinnerBoard()
        {
            SetInteractable(true);
            _winnerBoard.gameObject.SetActive(true);
        }

        private void OnFinishShowLoserBoard()
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