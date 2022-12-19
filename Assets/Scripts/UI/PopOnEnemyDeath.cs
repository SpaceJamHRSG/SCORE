using System;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class PopOnEnemyDeath : MonoBehaviour
    {
        [Range(1,2)][SerializeField] private float scaleTo;
        [SerializeField] private float scaleTime;

        private Vector3 _originalScale;
        private Vector3 _largeScale;
        private float _lerp;

        private void Awake()
        {
            _originalScale = transform.localScale;
            _largeScale = scaleTo * _originalScale;
        }

        private void OnEnable()
        {
            HealthEntity.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            HealthEntity.OnDeath -= HandleDeath;
        }

        private void Update()
        {
            _lerp -= Time.deltaTime / scaleTime;
            _lerp = Mathf.Clamp01(_lerp);
            float trueLerp = (1 - _lerp) * _lerp * _lerp + _lerp * (1 - (1 - _lerp) * (1 - _lerp));
            transform.localScale = Vector3.Lerp(_originalScale, _largeScale, trueLerp);
        }

        private void HandleDeath(int i, HealthEntity healthEntity, GameObject arg3)
        {
            if(healthEntity.Allegiance == Allegiance.Enemy)
                _lerp = 1;
        }
    }
}