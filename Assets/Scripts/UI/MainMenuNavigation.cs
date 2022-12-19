using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class MainMenuNavigation : MonoBehaviour
    {
        private RectTransform _activeMenu;
        [SerializeField] private RectTransform _startActiveMenu;
        [SerializeField] private AudioClip _cancelSound;

        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();
            _activeMenu = _startActiveMenu;
            _startActiveMenu.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_activeMenu != null && Input.GetMouseButtonDown(0))
            {
                _audio.PlayOneShot(_cancelSound);
                _activeMenu.gameObject.SetActive(false);
                _activeMenu = null;
            }
        }

        public void OpenMenu(RectTransform menu)
        {
            menu.gameObject.SetActive(true);
            _activeMenu = menu;
        }
    }
}