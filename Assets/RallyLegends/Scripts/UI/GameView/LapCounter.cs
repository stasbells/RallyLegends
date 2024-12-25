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

        public event UnityAction Finished;

        private void OnEnable()
        {
            _maxLapText.text = Constants.LapCount.ToString();
        }

        private void Update()
        {
            if (_targetCar == null)
                return;

            if (_targetCar.CurrentDistance < _previousDistance)
                OnLapComplete();

            if (_currentLap > Constants.LapCount)
                OnFinished();

            _previousDistance = _targetCar.CurrentDistance;
            _currentLapText.text = _currentLap.ToString();
        }

        public void SetTarget(AnimateCarAlongSpline target) => _targetCar = target;

        private void OnLapComplete() => _currentLap++;

        private void OnFinished()
        {
            _currentLap = 0;
            Finished?.Invoke();
        }
    }
}