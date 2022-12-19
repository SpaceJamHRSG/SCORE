using System;
using UnityEngine;

namespace AnimToFx
{
    public class PumpToTheBeat : MonoBehaviour
    {
        [SerializeField] private float delaySeconds;
        [SerializeField] private float BPM;
        [SerializeField] private float scaleAmount;
        [SerializeField] private AudioSource audioReference;

        private float _t;
        private float _trueT;

        private Vector3 _originalScale;

        private void Start()
        {
            _originalScale = transform.localScale;
        }

        private void Update()
        {
            float period = 60 / BPM;
            _t = period / 2 - Mathf.Abs((audioReference.time - delaySeconds) % period - period / 2);
            _t /= period/2;
            _trueT = _t * _t * _t;
            transform.localScale = Mathf.Lerp(1, scaleAmount, _trueT) * _originalScale;
        }
    }
}