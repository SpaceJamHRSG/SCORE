using System;
using Core;
using Entity;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Collider2D), typeof(PooledObject))]
    public class Projectile : MonoBehaviour
    {
        private const float DEFAULT_MAX_LIFETIME = 10;
        
        private PooledObject _pooledObject;

        private float _acceleration;
        private float _speed;
        private float _angularAcceleration;
        private float _angularVelocity;

        private Func<float, float> SpeedFunction;
        private Func<float, float> AngularFunction;

        private float _maxLifeTime;
        private float _timeSpawned;

        private int damage;
        private Allegiance _allegiance;

        private float _startingRotation;
        public Allegiance Allegiance { get; set; }
        public bool IsCritical { get; set; }

        public bool Penetrate;
        [SerializeField] private Sprite impactParticles;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
            _maxLifeTime = DEFAULT_MAX_LIFETIME;
        }

        private void OnEnable()
        {
            _timeSpawned = Time.time;
        }

        private void Update()
        {
            if (SpeedFunction == null)
            {
                _speed += _acceleration * Time.deltaTime;
            }
            else
            {
                _speed = SpeedFunction(Time.time - _timeSpawned);
            }
            transform.Translate(Vector2.up * _speed * Time.deltaTime
                                + Vector2.up * _acceleration * Time.deltaTime * Time.deltaTime);

            if (AngularFunction == null)
            {
                _angularVelocity += _angularAcceleration * Time.deltaTime;
                transform.Rotate(Vector3.forward, _angularVelocity * Time.deltaTime
                                                  + _angularAcceleration * Time.deltaTime * Time.deltaTime);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0,0,_startingRotation + AngularFunction(Time.time - _timeSpawned));
            }
            
            if (Time.time > _timeSpawned + _maxLifeTime)
            {
                if(_pooledObject.GetPool() == null) Destroy(gameObject);
                else Pooling.Instance.Despawn(_pooledObject);
            }
        }

        public void SetParams(float speed, float acceleration = 0, float angular = 0, float angularAcceleration = 0)
        {
            _speed = speed;
            _acceleration = acceleration;
            _angularVelocity = angular;
            _angularAcceleration = angularAcceleration;
        }

        public void SetStartingRotation(float sr)
        {
            _startingRotation = sr;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            HealthEntity other = col.GetComponent<HealthEntity>();
            if (other == null)
            {
                return;
            }
            if (other.Allegiance == this.Allegiance) return;
            other.TakeDamage(Math.Max(damage, 1), IsCritical, impactParticles);
            if(!Penetrate)
                Pooling.Instance.Despawn(_pooledObject);
        }

        public void SetSpeedOverTime(Func<float, float> speedFunction)
        {
            SpeedFunction = speedFunction;
        }

        public void SetAngularOverTime(Func<float, float> angularFunction)
        {
            AngularFunction = angularFunction;
        }

        public void SetDamage(int d)
        {
            damage = d;
        }
    }
}