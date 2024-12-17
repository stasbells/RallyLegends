using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RallyLegends.Data.Structs;

namespace RallyLegends.Sound
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private List<AudioSource> _audioSources;

        private float _volumeValue;

        public float VolumeValue => _volumeValue;

        private void Start()
        {
            OnVolumeChange();
        }

        public void AddSourse(AudioSource source) => _audioSources.Add(source);

        public void OnVolumeChange()
        {
            if (_volumeSlider != null)
            {
                foreach (AudioSource source in _audioSources)
                    source.volume = _volumeSlider.value;

                _volumeValue = _volumeSlider.value;
            }
            else
            {
                foreach (AudioSource source in _audioSources)
                    source.volume = _volumeValue;
            }
        }

        public void LoadData(SoundData soundData)
        {
            _volumeValue = soundData.VolumeValue;

            if (_volumeSlider != null)
                _volumeSlider.value = _volumeValue;
        }
    }
}