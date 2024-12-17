using UnityEngine;

namespace RallyLegends.CameraSettings
{
    public class CameraConstantWidth : MonoBehaviour
    {
        [SerializeField] private Vector2 _defaultResolution = new(720, 1280);
        [SerializeField, Range(0f, 1f)] private float _widthOrHeight = 0;

        private Camera _componentCamera;

        private float _targetAspect;
        private float _currentAspect;
        private float _initialFov;
        private float _horizontalFov = 120f;

        private void Start()
        {
            _componentCamera = GetComponent<Camera>();
            _currentAspect = _componentCamera.aspect;
            _initialFov = _componentCamera.fieldOfView;

            _targetAspect = _defaultResolution.x / _defaultResolution.y;
            _horizontalFov = CalculateVerticalFov(_initialFov, 1 / _targetAspect);

            AdjustFieldOfView();
        }

        private void Update()
        {
            if (_componentCamera.aspect != _currentAspect)
                AdjustFieldOfView();
        }

        private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
        {
            float horizontalFovInRads = horizontalFovInDeg * Mathf.Deg2Rad;
            float verticalFovInRads = 2 * Mathf.Atan(Mathf.Tan(horizontalFovInRads / 2) / aspectRatio);

            return verticalFovInRads * Mathf.Rad2Deg;
        }

        private void AdjustFieldOfView()
        {
            float constantWidthFov = CalculateVerticalFov(_horizontalFov, _componentCamera.aspect);

            _componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
            _currentAspect = _componentCamera.aspect;
        }
    }
}