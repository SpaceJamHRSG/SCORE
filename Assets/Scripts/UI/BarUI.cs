using System;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class BarUI : MonoBehaviour
    {
        private RectTransform _rt;
        private Vector3 _originalScale;

        private void Awake()
        {
            _rt = GetComponent<RectTransform>();
            _originalScale = _rt.localScale;
        }

        public void DisplayAt(float t)
        {
            t = Mathf.Clamp01(t);
            _rt.localScale = new Vector3(_originalScale.x, _originalScale.y * t, _originalScale.z);
        }
    }
}