using System;
using Core;
using Entity;
using Music;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Projectiles
{
    [RequireComponent(typeof(RhythmResponder))]
    public class ProjectileShooter : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float speed;
        [SerializeField] private float angularVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float angularAcceleration;
        [Space] [SerializeField] private bool enablePendulumMotion;
        [SerializeField] private float magnitude;
        [SerializeField] private float swingSpeed;
        [SerializeField] private float phase;
        
        private RhythmResponder _responder;
        public Allegiance Allegiance { get; set; }

        private void Awake()
        {
            _responder = GetComponent<RhythmResponder>();
        }

        private void Start()
        {
            _responder.SetTickResponse(Shoot, _responder.Line);
        }

        private void Shoot()
        {
            Projectile proj = Pooling.Instance.Spawn<Projectile>(projectilePrefab.gameObject, transform.position, transform.rotation);
            proj.SetParams(speed, acceleration, angularVelocity, angularAcceleration);
            if (enablePendulumMotion)
            {
                proj.SetAngularOverTime(t => MotionUtil.Pendulum(t, magnitude, swingSpeed, phase));
            }
            proj.Allegiance = Allegiance;
        }
    }
}