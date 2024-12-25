using System;
using UnityEngine;

namespace RallyLegends.Control
{
    [RequireComponent(typeof(AnimateCarAlongSpline))]
    public class BotSpeedController : MonoBehaviour, ISpeedController
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _acceleration;

        [Header("Speed Parameters")]
        [SerializeField] private float _speedLimit;
        [SerializeField] private float _increaseFactor;

        [Header("Distance Parameters")]
        [SerializeField] private float MaxOvertaking;
        [SerializeField] private float MaxRetard;

        private AnimateCarAlongSpline _targetCar;
        private AnimateCarAlongSpline _botCar;

        public float MaxSpeed => _maxSpeed;

        private void Start()
        {
            _botCar = GetComponent<AnimateCarAlongSpline>();
        }

        public float Change(float speed)
        {
            if (Distance() > MaxOvertaking)
                return Decrease(speed);

            if (Distance() < MaxRetard)
                return Increase(speed, _increaseFactor, _speedLimit);

            return Increase(speed);
        }

        public void SetTarget(AnimateCarAlongSpline target) => _targetCar = target;

        private float Distance() => _botCar.TotalDistance - _targetCar.TotalDistance;

        private float Decrease(float speed, float factor = 1f) => 
            Math.Clamp(speed - _acceleration * factor * Time.deltaTime, _minSpeed, _maxSpeed);

        private float Increase(float speed, float factor = 1f, float speedLimit = 1f) => 
            Math.Clamp(speed + _acceleration * factor * Time.deltaTime, _minSpeed, _maxSpeed * speedLimit);
    }
}