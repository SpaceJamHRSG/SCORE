using System;
using UnityEditor;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class WeaponUpgrade : IUpgrade
    {
        private WeaponDefinition _weapon;
        private int _levelUpTo;

        public WeaponUpgrade(WeaponDefinition w, int v)
        {
            _weapon = w;
            _levelUpTo = v;
        }

        public void Apply(PlayerManager player)
        {
            player.GrantWeaponLevel(_weapon, _levelUpTo);
        }

        public string GetUpgradeName()
        {
            return _weapon.GetWeaponInfoLevel(_levelUpTo).UpgradeName;
        }

        public string GetFlavourText()
        {
            return _weapon.GetWeaponInfoLevel(_levelUpTo).UpgradeDescription;
        }

        public Sprite GetImage()
        {
            return _weapon.GetWeaponInfoLevel(_levelUpTo).Image;
        }
    }

    
}