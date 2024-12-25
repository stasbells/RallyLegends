using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RallyLegends.Data;

namespace RallyLegends.GameLogic
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransition : MonoBehaviour
    {
        private static SceneTransition SceneTransitionInstance;

        [SerializeField] private TMP_Text _loadingPreñentage;
        [SerializeField] private Image _loadingProgressBar;

        private AsyncOperation _loadingSceneOperation;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            SceneTransitionInstance = this;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_loadingSceneOperation != null)
                LoadingPrecentageView();
        }

        public static void SwitchToScene(string sceneName)
        {
            SceneTransitionInstance._canvasGroup.alpha = 1f;
            SceneTransitionInstance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        }

        public void GoToGame() => SwitchToScene(Scene.SceneUtility.GetSceneNameByBuildIndex(Constants.GameSceneIndex));

        public void GoToMenu() => SwitchToScene(Scene.SceneUtility.GetSceneNameByBuildIndex(Constants.MenuSceneIndex));

        private void LoadingPrecentageView()
        {
            int precentFactor = 100;
            float durationAdjustmentValue = 5f;

            _loadingPreñentage.text = $"{Mathf.RoundToInt(_loadingSceneOperation.progress * precentFactor)}%";
            _loadingProgressBar.fillAmount = Mathf.Lerp(_loadingProgressBar.fillAmount,
                _loadingSceneOperation.progress, Time.deltaTime * durationAdjustmentValue);
        }
    }
}