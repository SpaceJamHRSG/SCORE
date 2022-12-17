using System;
using System.Collections.Generic;
using Entity;
using UnityEditor;
using UnityEngine;

namespace Projectiles
{
    [ExecuteInEditMode]
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