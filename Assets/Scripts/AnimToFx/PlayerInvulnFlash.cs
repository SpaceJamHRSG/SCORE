using System;
using System.Collections;
using UnityEngine;

namespace AnimToFx
{
    [RequireComponent(typeof(PlayerManager))]
    public class PlayerInvulnFlash : MonoBehaviour
    {
        [SerializeField] private float _flashPeriod;
        [SerializeField] private SpriteRenderer _spriteToFlash;

        private PlayerManager _player;
        private bool _isFlashing;

        private void Awake()
        {
            _player = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            _isFlashing = _player.IsInvulnerable;

            if (_isFlashing)
            {
                Color c = _spriteToFlash.color;
                c.a = Mathf.Sin(Time.time / _flashPeriod) / 2 + 1;
                _spriteToFlash.color = c;
            }
            else
            {
                _spriteToFlash.color = Color.white;
            }
            
        }
    }
}