using System;
using Entity;
using UnityEngine;

namespace AnimToFx
{
    [RequireComponent(typeof(Pickup))]
    public class PickupBlinkFX : MonoBehaviour
    {
        [SerializeField] private float _blinkIntensity;
        [SerializeField] private SpriteRenderer _sprite;
        private Pickup _pickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        private void Update()
        {
            if (_pickup.AboutToDespawn)
            {
                Color c = _sprite.color;
                c.a = (1 + Mathf.Sin(Time.time * _blinkIntensity)) / 2;
                c.a += 0.2f;
                _sprite.color = c;
            }
        }
    }
}