using UnityEngine;
using UnityEngine.UI;

namespace RallyLegends.Sound
{
    public class MuteView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _iconImage;

        [SerializeField] private Sprite _muteSprite;
        [SerializeField] private Sprite _simleSprite;

        private void Update()
        {
            if (_slider.gameObject.activeSelf)
                _iconImage.sprite = _slider.value == 0f ? _muteSprite : _simleSprite;
        }
    }
}