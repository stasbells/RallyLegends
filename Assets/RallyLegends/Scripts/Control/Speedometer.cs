using UnityEngine;
using TMPro;
using System;

namespace RallyLegends.Control
{
    public class Speedometer : MonoBehaviour
    {
        private const float MaxSpeed = 76f;

        [SerializeField] private RectTransform _arrow;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _start;

        private AnimateCarAlongSpline _target;

        private void Start()
        {
            _arrow.localRotation = Quaternion.Euler(0, 0, _start);
        }

        private void Update()
        {
            if (_target == null)
                return;

            _text.text = Convert.ToInt32(_target.CurrentSpeed).ToString();
            _arrow.localRotation = Quaternion.Euler(0, 0, GetPosition());
        }

        public void SetTarget(AnimateCarAlongSpline target) => _target = target;

        private float GetPosition()
        {
            float maxSpeed = MaxSpeed / _target.GetComponent<PlayerSpeedController>().MaxSpeed;

            return _start - _target.CurrentSpeed * maxSpeed;
        }
    }
}