using System;
using Entity;
using UnityEngine;

namespace SFX
{
    [RequireComponent(typeof(Pickup))]
    public class PickupSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip pickupSound;

        private Pickup _pickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        private void Start()
        {
            _pickup.OnPickup += PlayPickupSound;
        }

        private void PlayPickupSound()
        {
            GameManager.Instance.Audio.PlayOneShot(pickupSound);
        }
    }
}