using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class MainMenuNavigation : MonoBehaviour
    {
        private RectTransform _activeMenu;

        private void Awake()
        {
            _activeMenu = null;
        }

        private void Update()
        {
            if (_activeMenu != null && Input.GetMouseButtonDown(0))
            {
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