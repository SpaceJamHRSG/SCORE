using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Music
{
    public class RhythmParser
    {
        private float _bpmTracker;
        private float _timeTracker;
        public RhythmParser()
        {
            
        }

        public List<RhythmInstruction> ParseTextToInstructions(string script)
        {
            script = script.Replace("\n", " ");
            string[] textCommands = script.Split(' ');
            List<RhythmInstruction> instructions = new List<RhythmInstruction>();

            _bpmTracker = 1000; // super obvious if we forget to set bpm
            _timeTracker = 0;
            
            foreach (var command in textCommands)
            {
                RhythmInstruction instruction = ReadInstruction(command);
                if(instruction.Type != RhythmInstructionType.None)
                    instructions.Add(instruction);
            }
            instructions.Add(new RhythmInstruction(RhythmInstructionType.End, _timeTracker, 0));

            return instructions;
        }

        private RhythmInstruction ReadInstruction(string command)
        {
            float currentTime = _timeTracker;
            float value = 0;

            string timeString;
            string valueString;
            RhythmInstructionType instructionType;
            
            if (command.Equals(" ")) // empty command (e.g. due to extra whitespace)
            {
                instructionType = RhythmInstructionType.None;
                timeString = " ";
            }
            else if (command.Length >= 3 && command.Substring(0, 3) == "BPM") // change the bpm
            {
                instructionType = RhythmInstructionType.BPM;
                valueString = command.Substring(3);
                (float, bool) inputValue = ReadFraction(valueString);
                if (inputValue.Item2)
                {
                    value = inputValue.Item1;
                    _bpmTracker = value;
                }
            }
            else if (command.StartsWith('W')) // wait command (in seconds)
            {
                instructionType = RhythmInstructionType.Wait;
                timeString = command.Substring(1);
                (float, bool) inputValue = ReadFraction(timeString);
                if (inputValue.Item2)
                {
                    _timeTracker += inputValue.Item1;
                }
            }
            else // tick command (in beats according to bpm)
            {
                instructionType = RhythmInstructionType.Tick;
                timeString = command.Substring(0);
                (float, bool) inputValue = ReadFraction(timeString);
                if (inputValue.Item2)
                {
                    _timeTracker += Utility.BeatsToSeconds(inputValue.Item1, _bpmTracker);
                }
            }
            
            return new RhythmInstruction(instructionType, currentTime, value);
        }

        private (float, bool) ReadFraction(string s)
        {
            string[] divisors = s.Split("/");
            if (divisors.Length > 1)
            {
                if (float.TryParse(divisors[0], out float v1) && float.TryParse(divisors[1], out float v2))
                {
                    return (v1 / v2, true);
                }
                return (0, false);
            }
            if (float.TryParse(s, out float v))
            {
                return (v, true);
            }
            return (0, false);
        }
    }
}