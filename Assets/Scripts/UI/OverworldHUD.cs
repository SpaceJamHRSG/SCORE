using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OverworldHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI expTextField;
        [SerializeField] private TextMeshProUGUI levelTextField;
        [SerializeField] private BarUI expBar;
        [SerializeField] private List<TextMeshProUGUI> levelTexts;

        [SerializeField] private List<Image> weaponIcons;
        [SerializeField] private List<Sprite> availableWeaponSprites;
        [SerializeField] private List<Sprite> unavailableWeaponSprites;
        public void DisplayStats(PlayerManager player)
        {
            expTextField.text = $"{player.Exp} / {player.ExpToNext}";
            levelTextField.text = player.Level.ToString();
            
            expBar.DisplayAt((float)player.Exp/player.ExpToNext);

            levelTexts[0].text = player.GetWeaponLevelOf("lead").ToString();
            levelTexts[1].text = player.GetWeaponLevelOf("drums").ToString();
            levelTexts[2].text = player.GetWeaponLevelOf("rhythm").ToString();

            weaponIcons[0].sprite = player.GetWeaponLevelOf("lead") > 0 ? availableWeaponSprites[0] : unavailableWeaponSprites[0];
            weaponIcons[1].sprite = player.GetWeaponLevelOf("drums") > 0 ? availableWeaponSprites[1] : unavailableWeaponSprites[1];
            weaponIcons[2].sprite = player.GetWeaponLevelOf("rhythm") > 0 ? availableWeaponSprites[2] : unavailableWeaponSprites[2];
        }
    }
}