using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(PooledObject))]
    public class DamageNumberUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [Space]
        [SerializeField] private int leftBound;
        [SerializeField] private int rightBound;
        [SerializeField] private Color leftColor;
        [SerializeField] private Color rightColor;
        [SerializeField] private int leftScale;
        [SerializeField] private int rightScale;
        [Space] [SerializeField] private float leftDuration;
        [SerializeField] private float rightDuration;
        [Space] [SerializeField] private float rise;

        private float _duration;
        private float _timeOfSpawn;
        private Vector3 _positionOfSpawn;

        private PooledObject _pooledObject;

        private void Awake()
        {
            _pooledObject = GetComponent<PooledObject>();
        }

        private void OnEnable()
        {
            _timeOfSpawn = Time.time;
            _positionOfSpawn = transform.position;
        }

        public void SetDamageValue(int val)
        {
            textField.text = val.ToString();
            float damage01 = Mathf.InverseLerp(leftBound, rightBound, val);
            textField.fontSize = Mathf.Lerp(leftScale, rightScale,damage01);
            textField.color = Color.Lerp(leftColor, rightColor, damage01);
            _duration = Mathf.Lerp(leftDuration, rightDuration, damage01);
        }

        private void Update()
        {
            float time01 = Mathf.InverseLerp(_timeOfSpawn, _timeOfSpawn + _duration, Time.time);
            if(time01 > 1) Pooling.Instance.Despawn(_pooledObject);
            Color baseColor = textField.color;
            Color actualColor = new Color(baseColor.r, baseColor.g, baseColor.b, 1 - time01);
            textField.color = actualColor;

            Vector3 risePosition = _positionOfSpawn + Vector3.up * rise;
            transform.position = Vector3.Lerp(_positionOfSpawn, risePosition, time01);
        }
    }
}