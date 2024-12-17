using UnityEngine;
using RallyLegends.UI;

namespace RallyLegends.Control
{
    public class DustEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _rightWheel;
        [SerializeField] private ParticleSystem _leftWheel;

        private void OnEnable()
        {
            if (FindFirstObjectByType<StartScreen>())
                Stop();
            else
                Play();
        }

        private void OnDisable()
        {
            if (_leftWheel.isPlaying)
                Stop();
        }

        public void Play()
        {
            _leftWheel.Play();
            _rightWheel.Play();
        }

        private void Stop()
        {
            _leftWheel.Stop();
            _rightWheel.Stop();
        }
    }
}