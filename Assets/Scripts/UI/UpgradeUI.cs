using System;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private UpgradeSystem upgradeSystem;
        [SerializeField] private List<UpgradeSlotUI> upgradeSlots;
        [SerializeField] private TextMeshProUGUI levelTextField;
        [SerializeField] private UpgradeSlotUI healthRestoreSlot;

        private void OnEnable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress += HandleSelection;
            }

            healthRestoreSlot.OnPress += HandleSelection;
        }

        private void OnDisable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress -= HandleSelection;
            }

            healthRestoreSlot.OnPress -= HandleSelection;
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
            upgradeSystem.InvokeClose();
        }

        public void Refresh()
        {
            foreach (var s in upgradeSlots)
            {
                s.UpdateGraphics();
            }
            healthRestoreSlot.UpdateGraphics();
            levelTextField.text = upgradeSystem.ActivePlayer.Level.ToString();
        }

        public void SetUpgrades(List<IUpgrade> upgrades)
        {
            for (int i = 0; i < upgradeSlots.Count; i++)
            {
                upgradeSlots[i].Upgrade = null;
            }
            for (int i = 0; i < Math.Min(upgrades.Count, upgradeSlots.Count); i++)
            {
                upgradeSlots[i].Upgrade = upgrades[i];
            }
            
            if(upgradeSystem.ActivePlayer.Health != upgradeSystem.ActivePlayer.MaxHealth)
                healthRestoreSlot.Upgrade = new RestoreHPUpgrade(1);
            else
                healthRestoreSlot.Upgrade = new SimplePointsUpgrade(5000);
        }
    }
}