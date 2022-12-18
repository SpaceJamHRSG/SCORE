using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class StatUpgrade : IUpgrade
    {
        private Stat _type;
        private float _val;

        public StatUpgrade(Stat t, float i)
        {
            _type = t;
            _val = i;
        }
        
        public void Apply(PlayerManager player)
        {
            switch (_type.Type)
            {
                case StatType.Speed:
                    player.MoveSpeed = _val;
                    break;
                case StatType.BaseDamage:
                    player.Damage = _val;
                    break;
                case StatType.HP:
                    player.MaxHealth = (int)_val;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public string GetUpgradeName()
        {
            return _type.MainText;
        }

        public string GetFlavourText()
        {
            return _type.FlavourText;
        }

        public Sprite GetImage()
        {
            return _type.Image;
        }
    }
}