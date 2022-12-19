using System;
using System.Collections.Generic;
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
        [SerializeField] private bool clampAcceleration;
        [SerializeField] private float angularAcceleration;
        [Space] [SerializeField] private bool enablePendulumMotion;
        [SerializeField] private float magnitude;
        [SerializeField] private float swingSpeed;
        [SerializeField] private float phase;
        [SerializeField] private float maxBaseDamage;
        [SerializeField] [Range(0, 1)] private float baseDamageVariance;
        [SerializeField] [Range(0, 1)] private float baseCritChance;
        [SerializeField] private float baseCritStrength;
        [Space] [SerializeField] private bool autoTarget;
        [SerializeField] private int autoTargetRank;

        private float _damage;
        private float _damageVariance;
        private float _critChance;
        private float _critStrength;
        
        private RhythmResponder _responder;
        private Transform _target;
        public Allegiance Allegiance { get; set; }

        public bool AutoTarget
        {
            get => autoTarget;
            set { autoTarget = value; }
        }

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
            if (autoTarget)
            {
                EnemyDirector director = EnemyDirector.Instance;
                List<GameObject> spawnedEnemies = director.SpawnedEnemies;
                Transform[] closestFiveEnemies = new Transform[5];
                float[] closestFiveDistances = new float[5];
                for (int i = 0; i < 5; i++)
                {
                    closestFiveDistances[i] = float.PositiveInfinity;
                }

                foreach (var e in spawnedEnemies)
                {
                    float distance = (e.transform.position - transform.position).sqrMagnitude;
                    for (int i = 0; i < 5; i++)
                    {
                        if (distance < closestFiveDistances[i])
                        {
                            if (i != 4)
                            {
                                closestFiveDistances[i + 1] = closestFiveDistances[i];
                                closestFiveEnemies[i + 1] = closestFiveEnemies[i];
                            }

                            closestFiveDistances[i] = distance;
                            closestFiveEnemies[i] = e.transform;
                            break;
                        }
                    }
                }

                _target = closestFiveEnemies[autoTargetRank];
                if (_target == null)
                {
                    transform.rotation = Quaternion.LookRotation(UnityEngine.Random.insideUnitCircle, Vector3.forward);
                }
                else
                {
                    Vector2 diff = _target.transform.position - transform.position;
                    transform.rotation = Quaternion.Euler(0, 0, -180 / Mathf.PI * Mathf.Atan2(diff.x, diff.y));
                }
            }

            Projectile proj = Pooling.Instance.Spawn<Projectile>(projectilePrefab.gameObject, transform.position, transform.rotation);
            proj.SetStartingRotation(transform.rotation.eulerAngles.z);
            proj.SetParams(speed, acceleration, angularVelocity, angularAcceleration, clampAcceleration);
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