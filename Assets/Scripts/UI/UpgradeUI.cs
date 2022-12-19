using System;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using Random = UnityEngine.Random;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private UpgradeSystem upgradeSystem;
        [SerializeField] private List<UpgradeSlotUI> upgradeSlots;
        [SerializeField] private TextMeshProUGUI levelTextField;
        [SerializeField] private UpgradeSlotUI healthRestoreSlot;

        [SerializeField] private TooltipUI tooltip;
        [SerializeField] private AudioClip popupClip;

        private void OnEnable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress += HandleSelection;
            }

            healthRestoreSlot.OnPress += HandleSelection;

            int x = upgradeSystem.ActivePlayer.Level;
            int disp = x switch
            {
                2 => 0,
                3 => 1,
                _ => Random.Range(2, 102)
            };
            tooltip.Display(disp);
            
            GameManager.Instance.Audio.PlayOneShot(popupClip);
            GameManager.Instance.DisablePausing();
        }

        private void OnDisable()
        {
            foreach (var s in upgradeSlots)
            {
                s.OnPress -= HandleSelection;
            }

            healthRestoreSlot.OnPress -= HandleSelection;
        }

        private void Update()
        {
            Refresh(); // game jam code
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
            GameManager.Instance.EnablePausing();
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

            healthRestoreSlot.Upgrade = new SimplePointsUpgrade(5000);
        }
    }
}