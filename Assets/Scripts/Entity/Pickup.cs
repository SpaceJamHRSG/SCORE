using System;
using Core;
using Game;
using UnityEngine;

namespace Entity
{
    [RequireComponent(typeof(PooledObject), typeof(Collider2D))]
    public class Pickup : MonoBehaviour
    {
        public Action OnPickup;
        
        [SerializeField] private int expValue;
        
        [SerializeField] private float attractTimeSeconds;
        [SerializeField] private AnimationCurve attractionSpeedCurve;
        [SerializeField] private float attractionSpeedMax;
        [SerializeField] private float lifetime;
        private Func<float, float> _speedFunction;
        private Transform _attractTo;
        private float _firstAttractTime;
        private float _currentSpeed;

        private bool _attracted;
        private float _spawnTime;

        public bool AboutToDespawn => Time.time - _spawnTime > lifetime * 0.8f;

        private PooledObject _pooledObject;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
            _speedFunction = t =>
            {
                if (t > attractTimeSeconds) return attractionSpeedCurve.Evaluate(1);
                return attractionSpeedCurve.Evaluate(t / attractTimeSeconds);
            };
        }

        private void OnEnable()
        {
            _firstAttractTime = 0;
            _attractTo = null;
            _attracted = false;
            _currentSpeed = 0;
            _spawnTime = Time.time;
        }

        public void AttractTo(Transform p)
        {
            if (_attracted) return;
            _attractTo = p;
            _firstAttractTime = Time.time;
            _attracted = true;
        }

        private void Update()
        {
            if(Time.time - _spawnTime > lifetime && _attractTo == null) Pooling.Instance.Despawn(_pooledObject);
            if (_attractTo == null) return;
            _currentSpeed = _speedFunction(Time.time - _firstAttractTime);
            transform.position = Vector3.MoveTowards(transform.position, _attractTo.position, _currentSpeed * attractionSpeedMax * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ExpLevelEntity expLevelEntity = col.gameObject.GetComponent<ExpLevelEntity>();
            if (expLevelEntity == null) return;
            OnPickup?.Invoke();
            Pooling.Instance.Despawn(_pooledObject);
            expLevelEntity.GainExperience(expValue);
        }
    }
}