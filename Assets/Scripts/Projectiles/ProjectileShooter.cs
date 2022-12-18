using System;
using Core;
using Entity;
using Music;
using Unity.VisualScripting;
using UnityEngine;
using Utility;
using Random = System.Random;

namespace Projectiles
{
    [RequireComponent(typeof(RhythmResponder))]
    public class ProjectileShooter : MonoBehaviour
    {
        private Random random;
        
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float speed;
        [SerializeField] private float angularVelocity;
        [SerializeField] private float acceleration;
        [SerializeField] private float angularAcceleration;
        [Space] [SerializeField] private bool enablePendulumMotion;
        [SerializeField] private float magnitude;
        [SerializeField] private float swingSpeed;
        [SerializeField] private float phase;
        [SerializeField] private float maxBaseDamage;
        [SerializeField] [Range(0, 1)] private float baseDamageVariance;
        [SerializeField] [Range(0, 1)] private float baseCritChance;
        [SerializeField] private float baseCritStrength;

        private float _damage;
        private float _damageVariance;
        private float _critChance;
        private float _critStrength;
        
        private RhythmResponder _responder;
        public Allegiance Allegiance { get; set; }

        private void Awake()
        {
            _responder = GetComponent<RhythmResponder>();
            random = new Random();
        }

        private void Start()
        {
            _responder.SetTickResponse(Shoot, _responder.Line);
        }

        public void AssignDamageStats(float dmg, float variance, float crit, float critPow)
        {
            _damage = maxBaseDamage * dmg;
            _damageVariance = baseDamageVariance * variance;
            _critChance = 1 - (1 - crit) * (1 - baseCritChance);
            _critStrength = baseCritStrength * critPow;
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
            double randomDmg = random.NextDouble();
            int dmg = (int) (_damage * (1 - (float)(_damageVariance * randomDmg)));
            if (random.NextDouble() < _critChance)
            {
                dmg *= (int)(1 + _critStrength);
                proj.IsCritical = true;
            }
            proj.SetDamage(dmg);
        }

        public void SetProjectile(Projectile p)
        {
            projectilePrefab = p;
        }
    }
}