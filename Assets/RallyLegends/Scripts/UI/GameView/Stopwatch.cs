using System;
using TMPro;
using UnityEngine;

namespace RallyLegends.UI
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private TMP_Text _disvar;
        [SerializeField] private Countdown _countdown;
        [SerializeField] private LapCounter _lapCounter;

        private float _resault;
        private float _totalTime;
        private bool _srt = false;

        public bool Srt => _srt;
        public float Resault => _resault;

        private void Awake()
        {
            _totalTime = 0f;
            _srt = false;
        }

        private void Update()
        {
            if (_srt && _countdown.IsDone)
                _totalTime += Time.deltaTime;

            if (_resault < _totalTime)
                SaveTotalTime();

            TimeSpan time = TimeSpan.FromSeconds(_totalTime);
            _disvar.text = time.ToString(@"mm\:ss\:ff");
        }

        public void TimeReset()
        {
            SaveTotalTime();

            _srt = false;
            _totalTime = 0f;
        }

        public void TimeStop() => _srt = false;

        public void TimeStart() => _srt = true;

        private void SaveTotalTime() => _resault = _totalTime;
    }
}