using UnityEngine;
using UnityEngine.UI;

namespace RallyLegends.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _buttonClickSound;

        public void PlaySound(Button button) => _buttonClickSound.Play();
    }
}