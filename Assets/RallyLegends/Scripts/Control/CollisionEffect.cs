using System.Linq;
using UnityEngine;

namespace RallyLegends.Control
{
    public class CollisionEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explotionPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            GetExplotion(collision).gameObject.SetActive(true);
        }

        private ParticleSystem GetExplotion(Collision collision)
        {
            ContactPoint contact = collision.contacts.First();
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            return Instantiate(_explotionPrefab, pos, rot);
        }
    }
}