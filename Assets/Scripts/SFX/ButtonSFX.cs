using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SFX
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSFX : MonoBehaviour, ISelectHandler, IPointerEnterHandler
    {
        private AudioSource _audio;
        
        [SerializeField] private AudioClip _hoverAudio;
        [SerializeField] private AudioClip _selectAudio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            _audio.PlayOneShot(_selectAudio);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audio.PlayOneShot(_hoverAudio);
        }
    }
}