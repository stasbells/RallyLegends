using UnityEngine;
using RallyLegends.Control;
using RallyLegends.Data;
using UnityEngine.SceneManagement;

namespace RallyLegends.Sound
{
    [RequireComponent(typeof(AudioSource), typeof(AnimateCarAlongSpline))]
    public class CarEngineSound : MonoBehaviour
    {
        private const float DurationAdjustmentValue = 5f;

        [SerializeField] private AudioSource _engineSound;

        private AnimateCarAlongSpline _car;

        private void Start()
        {
            _car = GetComponent<AnimateCarAlongSpline>();
            _engineSound = GetComponent<AudioSource>();
            _engineSound.pitch = 0f;

            if (SceneManager.GetActiveScene().buildIndex == Constants.MenuSceneIndex)
                _engineSound.volume = 0f;
        }

        private void Update()
        {
            if (_car.isActiveAndEnabled)
                _engineSound.pitch = Mathf.Lerp(_engineSound.pitch, GetSoundPitchTarget(), Time.deltaTime * DurationAdjustmentValue);
        }

        private float GetSoundPitchTarget() 
        {
            float soundPitchAdjustmentValue = 15f;
            float soundPitchDivider = 70f;

            return (_car.CurrentSpeed + soundPitchAdjustmentValue) / soundPitchDivider;
        }
    }
}