using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileMultiShooter : MonoBehaviour
    {
        [SerializeField] private List<ProjectileShooter> shooters;
        [SerializeField] private Allegiance allegiance;

        private void Start()
        {
            foreach (ProjectileShooter shooter in shooters)
            {
                shooter.Allegiance = allegiance;
            }
        }
    }
}