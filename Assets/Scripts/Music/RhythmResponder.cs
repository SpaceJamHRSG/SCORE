using System;
using Music;
using UnityEngine;

namespace Music
{
    public class RhythmResponder : MonoBehaviour
    {
        [SerializeField] private string line;
        private Action TickResponse;

        public string Line => line;
        private void OnEnable()
        {
            RhythmEmitter.AddResponder(this);
        }

        private void OnDisable()
        {
            RhythmEmitter.RemoveResponder(this);
        }

        public void SetTickResponse(Action r, string l)
        {
            TickResponse = r;
            line = l;
        }

        public void Act()
        {
            if (TickResponse == null)
            {
                Debug.LogError("This rhythm responder has not yet been configured.");
                return;
            }
            TickResponse?.Invoke();
        }
    }
}