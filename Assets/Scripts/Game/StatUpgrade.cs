using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class StatUpgrade : IUpgrade
    {
        private StatType _type;
        private int _val;

        public StatUpgrade(StatType t, int i)
        {
            _type = t;
            _val = i;
        }


        public void Apply(PlayerManager player)
        {
            switch (_type)
            {
                case StatType.Speed:
                    player.BaseMoveSpeed = _val;
                    break;
                case StatType.BaseDamage:
                    player.BaseDamage = _val;
                    break;
                case StatType.HP:
                    player.BaseMaxHealth = _val;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}