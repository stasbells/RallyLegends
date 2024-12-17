using System;
using UnityEngine;

namespace RallyLegends.Control
{
    [RequireComponent(typeof(AnimateCarAlongSpline))]
    public class BotSpeedController : MonoBehaviour, ISpeedController
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;

        private const float MinSpeed = 10f;
        private const float SpeedLimit = 1.4f;
        private const float IncreaseFactor = 2f;
        private const float MaxOvertaking = 0.025f;
        private const float MaxRetard = -0.004f;

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
                return Increase(speed, IncreaseFactor, SpeedLimit);

            return Increase(speed);
        }

        public void SetTarget(AnimateCarAlongSpline target) => _targetCar = target;

        private float Distance() => _botCar.TotalDistance - _targetCar.TotalDistance;

        private float Decrease(float speed, float factor = 1f) => 
            Math.Clamp(speed - _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);

        private float Increase(float speed, float factor = 1f, float speedLimit = 1f) => 
            Math.Clamp(speed + _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed * speedLimit);
    }
}