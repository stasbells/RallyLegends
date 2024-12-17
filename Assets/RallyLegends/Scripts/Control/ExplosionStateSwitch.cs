using UnityEngine;

namespace RallyLegends.Control
{
    public class ExplosionStateSwitch : MonoBehaviour
    {
        private ExplosionEffect _explosionEffect;

        public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            _explosionEffect = GetComponentInChildren<ExplosionEffect>();
            AudioSource = GetComponentInChildren<AudioSource>();
            ExplosionEffectSetActive(false);
        }

        public void ExplosionEffectSetActive(bool value) => _explosionEffect.gameObject.SetActive(value);
    }
}