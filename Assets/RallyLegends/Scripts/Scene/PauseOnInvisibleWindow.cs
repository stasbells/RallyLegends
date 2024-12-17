using UnityEngine;
using YG;

namespace RallyLegends.Scene
{
    public class PauseOnInvisibleWindow : MonoBehaviour
    {
        private void OnEnable()
        {
            YandexGame.onShowWindowGame += OnShowWindowGame;
            YandexGame.onHideWindowGame += OnHideWindowGame;
        }

        private void OnDisable()
        {
            YandexGame.onShowWindowGame -= OnShowWindowGame;
            YandexGame.onHideWindowGame -= OnHideWindowGame;
        }

        public void OnShowWindowGame()
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }

        public void OnHideWindowGame()
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
    }
}