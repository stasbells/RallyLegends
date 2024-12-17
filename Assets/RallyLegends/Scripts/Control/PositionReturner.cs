using UnityEngine;

namespace RallyLegends.Control
{
    public class PositionReturner : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            _transform = transform;
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        private void OnDisable()
        {
            _transform.SetLocalPositionAndRotation(_startPosition, _startRotation);
        }
    }
}