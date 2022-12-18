using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnimToFx
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EntityAnimator : MonoBehaviour
    {
        private const float NON_ZERO_SPEED = 0.01f;
        
        private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private List<Sprite> _spriteSheetMovement;
        private int _movePtr;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_rb.velocity.sqrMagnitude > NON_ZERO_SPEED)
            {
                _movePtr++;
            }

            _sprite.sprite = _spriteSheetMovement[_movePtr % _spriteSheetMovement.Count];
        }
    }
}