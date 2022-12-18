using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class RestoreHPUpgrade : IUpgrade
    {
        private Stat _type;
        private float _val;

        public RestoreHPUpgrade(int quantity)
        {
            _val = quantity;
        }
        
        public void Apply(PlayerManager player)
        {
            player.Heal((int)_val);
        }

        public string GetUpgradeName()
        {
            return $"Restore {Math.Round(_val)} health.";
        }

        public string GetFlavourText()
        {
            return "";
        }

        public Sprite GetImage()
        {
            return _type.Image;
        }
    }
}