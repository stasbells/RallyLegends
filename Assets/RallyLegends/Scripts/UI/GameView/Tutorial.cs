using UnityEngine;
using UnityEngine.UI;
using YG;

namespace RallyLegends.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Countdown _countdown;
        [SerializeField] private Image _mobileImage;
        [SerializeField] private Image _desktopImage;

        private void Start()
        {
            _countdown.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SetActive);

            _mobileImage.gameObject.SetActive(YandexGame.EnvironmentData.isMobile);
            _desktopImage.gameObject.SetActive(!YandexGame.EnvironmentData.isMobile);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SetActive);
        }

        private void SetActive()
        {
            this.gameObject.SetActive(false);
            _countdown.gameObject.SetActive(true);
        }
    }
}