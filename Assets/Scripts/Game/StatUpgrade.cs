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
                    player.MoveSpeed *= 1 + _val;
                    break;
                case StatType.BaseDamage:
                    player.Damage *= 1 + _val;
                    break;
                case StatType.HP:
                    player.MaxHealth *= 1 + (int)_val;
                    break;
                case StatType.CritChance:
                    player.CritChance = 1 - (1 - player.CritChance) * (1 - _val);
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
            switch (_type.Type)
            {
                case StatType.Speed:
                    return $"Increases your speed by {Math.Round(_val * 100)}%";
                case StatType.BaseDamage:
                    return $"Increases your damage by {Math.Round(_val * 100)}%";
                case StatType.HP:
                    return $"Increases your HP by {Math.Round(_val * 100)}%";
                case StatType.CritChance:
                    return $"Increases your crit rate by {Math.Round(_val * 100)}%";
                default:
                    throw new ArgumentException();
            }
        }

        public Sprite GetImage()
        {
            return _type.Image;
        }

        public string GetTypeID()
        {
            return _type.ToString();
        }
    }
}