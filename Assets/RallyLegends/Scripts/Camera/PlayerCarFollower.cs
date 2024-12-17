using UnityEngine;

namespace RallyLegends.CameraSettings
{
    public class PlayerCarFollower : MonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private Vector3 _offset = new();

        private Transform _target;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_target != null)
                _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, Time.deltaTime * _smooth);
        }

        public void SetTarget(Transform target) => _target = target;
    }
}