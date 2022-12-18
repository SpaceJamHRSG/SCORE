using System;
using UnityEngine;

namespace AnimToFx
{
    public class SwayingMotion : MonoBehaviour
    {
        [SerializeField] private float _swayDegrees;
        [SerializeField] private float _swaySpeed;

        private float _timeStart;
        private float _center;

        private void OnEnable()
        {
            _timeStart = Time.time;
            _center = transform.rotation.z;
        }

        private void Update()
        {
            float t = Time.time - _timeStart;
            float theta = _center + _swayDegrees/2 * Mathf.Sin((float) (_swaySpeed * t + Math.PI/2));
            transform.rotation = Quaternion.Euler(0, 0, theta);
        }
    }
}