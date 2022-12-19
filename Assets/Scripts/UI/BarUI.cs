using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class BarUI : MonoBehaviour
    {
        private Image _rt;
        private Vector3 _originalScale;

        private void Awake()
        {
            _rt = GetComponent<Image>();
        }

        public void DisplayAt(float t)
        {
            t = Mathf.Clamp01(t);
            _rt.fillAmount = t;
        }
    }
}