using System;
using Core;
using Game;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(PooledObject), typeof(Collider2D))]
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private int expValue;
        
        [SerializeField] private float attractTimeSeconds;
        [SerializeField] private AnimationCurve attractionSpeedCurve;
        private Func<float, float> _speedFunction;
        private Transform _attractTo;
        private float _firstAttractTime;
        private float _currentSpeed;

        private PooledObject _pooledObject;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
            _speedFunction = t =>
            {
                if (t > attractTimeSeconds) return attractionSpeedCurve.Evaluate(attractTimeSeconds);
                return attractionSpeedCurve.Evaluate(t);
            };
        }

        private void OnEnable()
        {
            _firstAttractTime = 0;
            _attractTo = null;
        }

        public void AttractTo(Transform p)
        {
            _attractTo = p;
            _firstAttractTime = Time.time;
        }

        private void Update()
        {
            if (_attractTo == null) return;
            _currentSpeed = _speedFunction(Time.time - _firstAttractTime);
            transform.position = Vector3.MoveTowards(transform.position, _attractTo.position, _currentSpeed);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Pooling.Instance.Despawn(_pooledObject);
        }
    }
}