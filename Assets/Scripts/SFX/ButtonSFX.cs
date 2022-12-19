using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SFX
{
    public class ButtonSFX : MonoBehaviour, IPointerEnterHandler
    {
        private AudioSource _audio;
        
        [SerializeField] private AudioClip _hoverAudio;
        [SerializeField] private AudioClip _selectAudio;

        private void Awake()
        {
            if (GameManager.Instance == null || GameManager.Instance.Audio == null)
                _audio = GetComponent<AudioSource>();
            else _audio = GameManager.Instance.Audio;
        }

        public void OnSelect()
        {
            _audio.PlayOneShot(_selectAudio);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audio.PlayOneShot(_hoverAudio);
        }
    }
}