using System;
using UnityEngine;
using RallyLegends.UI;

namespace RallyLegends.Control
{
    public class PlayerSpeedController : MonoBehaviour, ISpeedController
    {
        [SerializeField] ExplosionStateSwitch _explosionSwitch;

        private const float MinSpeed = 0f;
        private const float MaxSpeedFactor = 0.2f;
        private const float OverheatingFactor = 1.5f;

        private float _counter = 0f;
        private float _overheatingSpeedValue = 0.8f;

        private bool _isOverheating = false;

        private OverheatWarning _overheatWarning;
        private PlayerInput _playerInput;

        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float OverheatingTime { get; private set; }

        private void Awake()
        {
            _overheatingSpeedValue *= MaxSpeed;
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _counter = 0f;
            _playerInput.Disable();
        }

        public float Change(float speed)
        {
            if (speed < _overheatingSpeedValue)
                _counter = 0f;

            if (speed < 5f)
            {
                _isOverheating = false;
                _explosionSwitch.ExplosionEffectSetActive(false);
            }

            _overheatWarning.ActiveBy(_counter);

            if (_playerInput.Player.Move.ReadValue<float>() > 0.1f)
            {
                if (speed >= _overheatingSpeedValue && !_isOverheating)
                {
                    ÑountUntilOverheating();

                    return Increase(speed, MaxSpeedFactor);
                }
                else
                {
                    return _isOverheating ? Decrease(speed, OverheatingFactor) : Increase(speed);
                }
            }
            else
            {
                return Decrease(speed);
            }
        }

        public void SetOverheatWarning(OverheatWarning overheatWarning) => _overheatWarning = overheatWarning;

        private float Increase(float speed, float factor = 1f) => Math.Clamp(speed + Acceleration * factor * Time.deltaTime, MinSpeed, MaxSpeed);

        private float Decrease(float speed, float factor = 1f) => Math.Clamp(speed - Acceleration * factor * Time.deltaTime, MinSpeed, MaxSpeed);

        private void ÑountUntilOverheating()
        {
            _counter += Time.deltaTime;

            if (_counter > OverheatingTime)
            {
                _isOverheating = true;
                _explosionSwitch.ExplosionEffectSetActive(true);
            }
        }
    }
}