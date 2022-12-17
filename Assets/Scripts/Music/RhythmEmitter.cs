using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Projectiles;
using UnityEngine;

namespace Music
{
    public class RhythmEmitter : MonoBehaviour
    {
        public static Action<RhythmLine> OnTick;
        public static Action<RhythmLine> OnEnd;
        [SerializeField] private List<RhythmLine> _rhythmLines;

        private static List<RhythmResponder> _responders;

        private void OnEnable()
        {
            foreach (var line in _rhythmLines)
            {
                line.OnTick += Tick;
                line.OnEnd += Tick;
            }
        }
        
        private void OnDisable()
        {
            foreach (var line in _rhythmLines)
            {
                line.OnTick -= Tick;
                line.OnEnd -= Tick;
            }
        }

        public void Tick(RhythmLine fromLine)
        {
            OnTick?.Invoke(fromLine);
            foreach (var r in _responders)
            {
                if (r.Line.Equals(fromLine.LineTag))
                {
                    r.Act();
                }
            }
        }

        public static void AddResponder(RhythmResponder res)
        {
            if (_responders == null) _responders = new List<RhythmResponder>();
            _responders.Add(res);
        }

        public static void RemoveResponder(RhythmResponder res)
        {
            _responders.Remove(res);
        }

        private void InitAllLines()
        {
            foreach (var line in _rhythmLines)
            {
                line.Parse();
            }
        }
        private void StartAllLines()
        {
            foreach (var line in _rhythmLines)
            {
                line.Play();
            }
        }
        
        private void StopAllLines()
        {
            foreach (var line in _rhythmLines)
            {
                line.Stop();
            }
        }
        
        private void PauseAllLines()
        {
            foreach (var line in _rhythmLines)
            {
                line.Pause();
            }
        }
    }
}
