using UnityEngine;

namespace RallyLegends.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _buttonClickSound;
    }
}