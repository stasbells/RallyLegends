using TMPro;
using UnityEngine;
using UnityEngine.Events;
using RallyLegends.Data;
using RallyLegends.Control;

namespace RallyLegends.UI
{
    public class LapCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentLapText;
        [SerializeField] private TMP_Text _maxLapText;

        private int _currentLap = 1;
        private float _previousDistance = 0f;
        private AnimateCarAlongSpline _targetCar;

        public event UnityAction Finised;
        public event UnityAction LapComplete;

        private void OnEnable()
        {
            _maxLapText.text = Constants.LapCount.ToString();
        }

        private void Update()
        {
            if (_targetCar == null)
                return;

            if (_targetCar.CurrentDistance < _previousDistance)
            {
                _currentLap++;
                LapComplete?.Invoke();
            }

            if (_currentLap > Constants.LapCount)
            {
                _currentLap = 0;
                Finised?.Invoke();
            }

            _previousDistance = _targetCar.CurrentDistance;
            _currentLapText.text = _currentLap.ToString();
        }

        public void SetTarget(AnimateCarAlongSpline target) => _targetCar = target;
    }
}