using System;
using Entity;
using UnityEngine;

namespace SFX
{
    [RequireComponent(typeof(Pickup), typeof(AudioSource))]
    public class PickupSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip pickupSound;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}