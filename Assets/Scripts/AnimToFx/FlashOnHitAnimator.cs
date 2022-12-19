using System;
using System.Collections;
using Entity;
using UnityEngine;

namespace AnimToFx
{
    [RequireComponent(typeof(HealthEntity))]
    public class FlashOnHitAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private Material flashMaterial;
        [SerializeField] private Material standardMaterial;
        [SerializeField] private float flashTime;
        
        private HealthEntity _health;
        private bool _flashLock;

        private void Awake()
        {
            _health = GetComponent<HealthEntity>();
        }

        private void OnEnable()
        {
            _health.OnThisHit += HandleHit;
        }

        private void OnDisable()
        {
            _health.OnThisHit -= HandleHit;
        }
        
        private void HandleHit (int i, bool crit, GameObject _)
        {
            StartCoroutine(FlashSequence(flashTime));
        }

        IEnumerator FlashSequence(float dur)
        {
            if (_flashLock) yield break;
            _flashLock = true;
            renderer.material = flashMaterial;
            yield return new WaitForSeconds(dur);
            renderer.material = standardMaterial;
            _flashLock = false;
        }
    }
}