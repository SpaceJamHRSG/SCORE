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
        public Action<RhythmLine> OnTick;
        public Action<RhythmLine> OnEnd;

        [SerializeField] private string lineTag;
        [SerializeField] private AudioClip audio;
        [SerializeField] private TextAsset script;
        [TextArea(10, 20)]
        [SerializeField] private string scriptField;
        
        private AudioSource _audioSource;
        private List<RhythmInstruction> _instructions;
        private RhythmParser _parser;

        private bool _parsed;
        private int _ptr;

        public string LineTag => lineTag;

        private void Start()
        {
            Initialize();
            _audioSource.clip = audio;
            _parsed = false;
        }

        private void Update()
        {
            //DebugParser();
            //ParseOnSpacePressed();
            if (_parsed)
            {
                CheckInstructions();
            }
        }
        
        public void Play()
        {
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
        
        public void Pause()
        {
            _audioSource.Pause();
        }

        private void Initialize()
        {
            _parser = new RhythmParser();
            _audioSource = GetComponent<AudioSource>();
            _ptr = 0;
        }
        
        public void Parse()
        {
            Initialize();
            if(script != null)
                _instructions = _parser.ParseTextToInstructions(script.ToString());
            else
                _instructions = _parser.ParseTextToInstructions(scriptField.ToString());
            _parsed = true;
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
            
            while (currentTime < timeOfLastInstruction && _ptr > 0) //song was rewound somehow
            {
                _ptr--;
                timeOfLastInstruction = _instructions[_ptr].Time;
            }

            if (_ptr == _instructions.Count - 1) return;
            
            RhythmInstruction nextInstruction = _instructions[_ptr + 1];
            float timeOfNextInstruction = nextInstruction.Time;
            
            while (currentTime >= timeOfNextInstruction && _ptr < _instructions.Count - 1) //forward passage of time
            {
                switch (nextInstruction.Type)
                {
                    case RhythmInstructionType.Tick:
                        OnTick?.Invoke(this);
                        break;
                    case RhythmInstructionType.End:
                        OnEnd?.Invoke(this);
                        Debug.Log("This line is over.");
                        break;
                }
                
                _ptr++;
                if (_ptr < _instructions.Count - 1)
                {
                    nextInstruction = _instructions[_ptr + 1];
                    timeOfNextInstruction = nextInstruction.Time;
                }
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
        
        private void ParseOnSpacePressed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Parse(scriptField);
                Play();
            }
        }

        public void SetVolume(float f)
        {
            _audioSource.volume = f;
        }
        
        public float GetVolume()
        {
            return _audioSource.volume;
        }
        
    }
}