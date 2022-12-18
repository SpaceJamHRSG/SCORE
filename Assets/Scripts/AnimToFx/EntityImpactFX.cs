using System;
using Entity;
using UnityEngine;

namespace AnimToFx
{
    [RequireComponent(typeof(HealthEntity))]
    public class EntityImpactFX : MonoBehaviour
    {
        private HealthEntity _health;
        [SerializeField] private GameObject _hitPrefabDefault;

        private void Awake()
        {
            _health = GetComponent<HealthEntity>();
        }

        private void OnEnable()
        {
            _health.OnThisHit += RespondToHit;
        }
        
        private void OnDisable()
        {
            _health.OnThisHit -= RespondToHit;
        }

        private void RespondToHit(int dmg, bool crit, GameObject impactParticles)
        {
            if (impactParticles != null)
            {
                Instantiate(impactParticles, transform.position, transform.rotation);
            }
            else if (_hitPrefabDefault != null)
            {
                Instantiate(_hitPrefabDefault, transform.position, transform.rotation);
            }
        }
    }
}