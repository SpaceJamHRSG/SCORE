using System;
using UnityEngine;

namespace AnimToFx
{
    public class ExpandThenDisappear : MonoBehaviour
    {
        [SerializeField] private float _maxSize;
        [SerializeField] private float _timeToDeath;
        [SerializeField] private SpriteRenderer _sprite;

        private float _timer;
        private Vector3 _originalScale;
        private Color _originalColor;
        

        private void OnEnable()
        {
            _timer = 0;
            _originalColor = _sprite.color;
            _originalScale = transform.localScale;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            Color newColor = _originalColor;
            float t = _timer / _timeToDeath;
            newColor.a = Mathf.Lerp(1, 0, t);
            transform.localScale = Vector3.Lerp(_originalScale, _maxSize * _originalScale, t);
            if(_timer >= _timeToDeath) Destroy(gameObject);
        }
    }
}