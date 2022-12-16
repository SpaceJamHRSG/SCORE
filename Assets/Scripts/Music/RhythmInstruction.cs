using System;
using UnityEngine;

namespace Music
{
    public enum RhythmInstructionType
    {
        Tick, Wait, BPM, None
    }
    public struct RhythmInstruction
    {
        public RhythmInstructionType Type;
        public float Value;
        private float time;

        public float Time
        {
            get
            {
                switch (Type)
                {
                    case RhythmInstructionType.Tick:
                    case RhythmInstructionType.Wait:
                        Debug.LogWarning("Duration of beat cannot be negative. Defaulting to 0.");
                        return Math.Max(0, time);
                    default:
                        return time;
                }   
            }
        }

        public RhythmInstruction(RhythmInstructionType type, float time, float value)
        {
            Type = type;
            this.time = time;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"({Type}, {time}, {Value}).";
        }
    }
}