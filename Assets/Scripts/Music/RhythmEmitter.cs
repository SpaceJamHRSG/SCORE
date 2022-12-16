using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Music
{
    public class RhythmEmitter : MonoBehaviour
    {
        public static Action<RhythmLine> OnTick;
        private List<RhythmLine> _rhythmLines;

        public void Tick(RhythmLine fromLine)
        {
            OnTick?.Invoke(fromLine);
        }
    }
}
