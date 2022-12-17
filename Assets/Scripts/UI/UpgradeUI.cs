using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private UpgradeSystem upgradeSystem;
        [SerializeField] private List<UpgradeSlotUI> upgradeSlots;

        private void OnEnable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress += HandleSelection;
            }
        }

        private void OnDisable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress -= HandleSelection;
            }
        }

        private void HandleSelection(UpgradeSlotUI usui)
        {
            upgradeSystem.ApplyUpgrade(usui.Upgrade, upgradeSystem.ActivePlayer);
            Close();
        }

        private void Close()
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}