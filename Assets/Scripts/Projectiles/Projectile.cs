using System;
using Core;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        private ObjectPool _myObjectPool;
        
        [SerializeField] private float baseSpeed;
        [SerializeField] private float baseRotation;

        private float _acceleration;
        private float _speed;
        private float _angularAcceleration;
        private float _angularVelocity;

        private void Update()
        {
            _speed += _acceleration * Time.deltaTime;
            _angularVelocity += _angularVelocity * Time.deltaTime;

            transform.Translate(Vector2.up * _speed * Time.deltaTime 
                                + Vector2.up * _acceleration * Time.deltaTime * Time.deltaTime);
            transform.Rotate(Vector3.forward, _angularVelocity * Time.deltaTime 
                                              + _angularAcceleration * Time.deltaTime * Time.deltaTime);
        }

        public void SetParams(float speed, float acceleration = 0, float angular = 0, float angularAcceleration = 0)
        {
            _speed = speed;
            _acceleration = acceleration;
            _angularVelocity = angular;
            _angularAcceleration = angularAcceleration;
        }

        
    }
}