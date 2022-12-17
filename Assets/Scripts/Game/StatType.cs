using UnityEngine;

namespace Game
{
    public enum StatType
    {
        Speed, BaseDamage, HP
    }

    [CreateAssetMenu(fileName = "Player Stat", menuName = "Stat")]
    public class Stat : ScriptableObject
    {
        public StatType Type;
        public string MainText;
        public string FlavourText;
        public Sprite Image;
        public float BaseUpgradeUnit;
    }
}