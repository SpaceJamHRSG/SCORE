using UnityEngine;

namespace Game
{
    public interface IUpgrade
    {
        public void Apply(PlayerManager player);
        public string GetUpgradeName();
        public string GetFlavourText();
        public Sprite GetImage();
    }
}