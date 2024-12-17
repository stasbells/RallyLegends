using UnityEngine;
using RallyLegends.UI;

namespace RallyLegends.Control
{
    [RequireComponent(typeof(AnimateCarAlongSpline))]
    public class SelectedCarAnimation : MonoBehaviour
    {
        private float _startScreenAlpha;
        private float _garageScreenAlpha;

        private void OnEnable()
        {
            if (FindFirstObjectByType<StartScreen>())
            {
                _startScreenAlpha = FindFirstObjectByType<StartScreen>().GetComponent<CanvasGroup>().alpha;
                _garageScreenAlpha = FindFirstObjectByType<GarageScreen>().GetComponent<CanvasGroup>().alpha;
            }
        }

        private void Update()
        {
            if (_garageScreenAlpha == 1f || _startScreenAlpha == 1f)
                transform.RotateAround(gameObject.transform.position, Vector3.up, 20 * Time.deltaTime);
        }
    }
}