using System;
using UnityEditor;

namespace Game
{
    [Serializable]
    public class WeaponUpgrade : IUpgrade
    {
        private WeaponDefinition _weapon;
        private int _levelUpBy;

        public WeaponUpgrade(WeaponDefinition w, int v)
        {
            _weapon = w;
            _levelUpBy = v;
        }

        public void Apply(PlayerManager player)
        {
            player.LevelUpWeapon(_weapon, _levelUpBy);
        }
    }

    
}