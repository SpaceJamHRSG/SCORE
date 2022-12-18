using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class OverworldHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthTextField;
        [SerializeField] private TextMeshProUGUI expTextField;
        [SerializeField] private TextMeshProUGUI levelTextField;
        [SerializeField] private BarUI healthBar;
        [SerializeField] private BarUI expBar;
        [SerializeField] private List<TextMeshProUGUI> levelTexts;


        public void DisplayStats(PlayerManager player)
        {
            healthTextField.text = $"{player.Health} / {player.MaxHealth}";
            expTextField.text = $"{player.Exp} / {player.ExpToNext}";
            levelTextField.text = player.Level.ToString();
            
            healthBar.DisplayAt((float)player.Health/player.MaxHealth);
            expBar.DisplayAt((float)player.Exp/player.ExpToNext);

            levelTexts[0].text = player.GetWeaponLevelOf("lead").ToString();
            levelTexts[1].text = player.GetWeaponLevelOf("drums").ToString();
            levelTexts[2].text = player.GetWeaponLevelOf("rhythm").ToString();
        }
    }
}