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
        [SerializeField] private RectTransform inverseExpBar;
        [SerializeField] private List<TextMeshProUGUI> levelTexts;
        [SerializeField] private TextMeshProUGUI scoreDisplay;

        [SerializeField] private List<Image> weaponIcons;
        [SerializeField] private List<Sprite> availableWeaponSprites;
        [SerializeField] private List<Sprite> unavailableWeaponSprites;
        
        public void DisplayStats(PlayerManager player)
        {
            expTextField.text = $"{player.Exp} / {player.ExpToNext}";
            levelTextField.text = player.Level.ToString();
            
            expBar.DisplayAt((float)player.Exp/player.ExpToNext);

            string scoreString = GameManager.Instance.TotalScore.ToString().PadLeft(8, '0');
            scoreDisplay.text = scoreString;

            int leadLevel = player.GetWeaponLevelOf("lead");
            int drumLevel = player.GetWeaponLevelOf("drums");
            int rhythmLevel = player.GetWeaponLevelOf("rhythm");

            levelTexts[0].text = leadLevel >= 3 ? "MAXED!" : leadLevel == 0 ? "" :
                $"LVL {leadLevel.ToString()}";
            levelTexts[1].text = drumLevel >= 3 ? "MAXED!" : drumLevel == 0 ? "" :
                $"LVL {drumLevel.ToString()}";
            levelTexts[2].text = rhythmLevel >= 3 ? "MAXED!" : rhythmLevel == 0 ? "" :
                $"LVL {rhythmLevel.ToString()}";

            weaponIcons[0].sprite = leadLevel > 0 ? availableWeaponSprites[0] : unavailableWeaponSprites[0];
            weaponIcons[1].sprite = drumLevel > 0 ? availableWeaponSprites[1] : unavailableWeaponSprites[1];
            weaponIcons[2].sprite = rhythmLevel > 0 ? availableWeaponSprites[2] : unavailableWeaponSprites[2];
            
        }
    }
}