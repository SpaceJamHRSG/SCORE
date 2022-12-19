using System;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace SFX
{
    [RequireComponent(typeof(HealthEntity), typeof(AudioSource))]
    public class EntitySFX : MonoBehaviour
    {
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip onHitSound;

        private HealthEntity _health;
        private AudioSource _audio;
        private void OnEnable()
        {
            _health.OnThisHit += PlayHitSound;
            _health.OnThisDeath += PlayDeathSound;
        }
        private void OnDisable()
        {
            _health.OnThisHit -= PlayHitSound;
            _health.OnThisDeath -= PlayDeathSound;
        }

        private void PlayHitSound(int i, bool b, GameObject arg3)
        {
            if(onHitSound != null)
                _audio.PlayOneShot(onHitSound);
        }

        private void PlayDeathSound(int i)
        {
            if(deathSound != null)
                _audio.PlayOneShot(deathSound);
        }
    }
}