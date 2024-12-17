using UnityEngine;

namespace RallyLegends.Control
{
    [RequireComponent(typeof(AnimateCarAlongSpline))]
    public class CollisionController : MonoBehaviour
    {
        private const float ImpactForce = 1.5f;

        private AnimateCarAlongSpline _animateCarAlongSpline;

        private void Awake()
        {
            _animateCarAlongSpline = GetComponentInParent<AnimateCarAlongSpline>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.GetComponent<Rigidbody>()?.AddForce
                (_animateCarAlongSpline.CurrentSpeed * ImpactForce * (transform.forward + Vector3.up));
        }
    }
}