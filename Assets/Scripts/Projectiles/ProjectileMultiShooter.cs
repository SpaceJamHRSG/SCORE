using System;
using System.Collections.Generic;
using Entity;
using Music;
using UnityEditor;
using UnityEngine;

namespace Projectiles
{
    [ExecuteInEditMode]
    public class ProjectileMultiShooter : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private List<ProjectileShooter> shooters;
        [SerializeField] private Allegiance allegiance;
        private void Start()
        {
            foreach (ProjectileShooter shooter in shooters)
            {
                shooter.Allegiance = allegiance;
            }
        }

        private void Update()
        {
            foreach (var s in shooters)
            {
                s.transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
            }
        }

        public void SetPart(string part)
        {
            foreach (var s in shooters)
            {
                s.GetComponent<RhythmResponder>().Line = part;
            }
        }

        public void SetProjectile(Projectile p)
        {
            foreach (var s in shooters)
            {
                s.SetProjectile(p);
            }
        }
        
        public void AssignDamageStats(float dmg, float variance, float crit, float critPow)
        {
            foreach (var s in shooters)
            {
                s.AssignDamageStats(dmg, variance, crit, critPow);
            }
        }
    }
}