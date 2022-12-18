using System;
using System.Collections.Generic;
using UnityEngine;

namespace Music
{
    public class RhythmManager : MonoBehaviour
    {
        [SerializeField] private RhythmEmitter mainAudio;
        [SerializeField] private RhythmEmitter restAudio;
        [SerializeField] private float crossFadeSpeed;
        [SerializeField] private bool debug;
        private Dictionary<RhythmEmitter, float> _targetVolume;

        public RhythmEmitter MainAudio => mainAudio;

        private void Awake()
        {
            _targetVolume = new Dictionary<RhythmEmitter, float>()
            {
                {mainAudio, 0}, {restAudio, 0}
            };
        }

        public void StartMainAudio()
        {
            mainAudio.InitAllLines();
            restAudio.InitAllLines();
            _targetVolume[mainAudio] = 1;
            mainAudio.PlayAllLines();
        }

        private void Update()
        {
            if (debug)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    StartMainAudio();
                }
            }
            foreach (var kvp in _targetVolume)
            {
                kvp.Key.SetVolume(Mathf.Lerp(kvp.Key.GetVolume(), kvp.Value, crossFadeSpeed * Time.deltaTime));
            }
        }

        public void FadeToMainAudio()
        {
            restAudio.PauseAllLines();
            mainAudio.PlayAllLines();
            _targetVolume[restAudio] = 0;
            _targetVolume[mainAudio] = 1;
        }
        
        public void FadeToRestAudio()
        {
            mainAudio.PauseAllLines();
            restAudio.PlayAllLines();
            _targetVolume[mainAudio] = 0;
            _targetVolume[restAudio] = 1;
        }
    }
}