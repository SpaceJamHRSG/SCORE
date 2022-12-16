using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Music
{
    [RequireComponent(typeof(AudioSource))]
    public class RhythmLine : MonoBehaviour
    {
        public Action OnTick;
        
        [SerializeField] private AudioClip audio;
        [SerializeField] private TextAsset script;
        [SerializeField] private string scriptField;
        private AudioSource _audioSource;
        private List<RhythmInstruction> _instructions;
        private RhythmParser _parser;

        private bool _parsed;
        private int _ptr;

        private void Start()
        {
            Initialize();
            _parsed = false;
        }

        private void Update()
        {
            //DebugParser();
            CheckInstructions();
        }
        
        public void Play()
        {
            _audioSource.Play();
        }

        private void Initialize()
        {
            _parser = new RhythmParser();
            _audioSource = GetComponent<AudioSource>();
            _ptr = 0;
        }
        
        public void Parse(string text)
        {
            Initialize();
            _instructions = _parser.ParseTextToInstructions(text);
            _parsed = true;
        }

        private void CheckInstructions()
        {
            float currentTime = _audioSource.time;
            float timeOfLastInstruction = _instructions[_ptr].Time;
            float timeOfNextInstruction = _instructions[_ptr + 1].Time;
            
            while (currentTime >= timeOfNextInstruction && _ptr < _instructions.Count - 1) //forward passage of time
            {
                _ptr++;
                timeOfNextInstruction = _instructions[_ptr + 1].Time;
                OnTick?.Invoke();
            }
            while (currentTime < timeOfLastInstruction && _ptr > 0) //song was rewound somehow
            {
                _ptr--;
                timeOfLastInstruction = _instructions[_ptr].Time;
            }
        }
        
        private void DebugParser()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Parse(scriptField);
                foreach (var s in _instructions)
                {
                    Debug.Log(s.ToString());
                }
            }
        }
    }
}