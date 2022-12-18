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
        [SerializeField] private List<Sprite> _spriteSheetIdle;
        [SerializeField] private float _animationSpeedSeconds; // sync to the goddamn bpm
        private float _moveTimer;
        private bool _moveState;
        private int _ptr;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _moveTimer += Time.deltaTime / _animationSpeedSeconds;
            _moveState = _rb.velocity.sqrMagnitude > NON_ZERO_SPEED;
            
            if (_moveTimer > 1)
            {
                _ptr++;
                _moveTimer = 0;
            }

            _sprite.sprite = _moveState ? 
                _spriteSheetMovement[_ptr % _spriteSheetMovement.Count] : 
                _spriteSheetIdle[_ptr % _spriteSheetMovement.Count];
        }
    }
}