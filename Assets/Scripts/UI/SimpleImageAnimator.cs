using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class SimpleImageAnimator : MonoBehaviour
    {
        [SerializeField] private float _animIntervalSeconds;
        [SerializeField] private List<Sprite> _sprites;

        private Image _img;
        private float _timer;
        private int _ptr;

        private void Awake()
        {
            _img = GetComponent<Image>();
            _timer = 0;
            _ptr = 0;
            _img.sprite = _sprites[0];
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _animIntervalSeconds)
            {
                _timer = 0;
                _ptr++;
                _img.sprite = _sprites[_ptr % _sprites.Count];
            }
        }
    }
}