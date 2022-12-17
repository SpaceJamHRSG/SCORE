using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Player Stats", order = 0)]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int baseDamage;
        [SerializeField] private int baseSpeed;
        
        public int MaxHealth => maxHealth;
        public int BaseDamage => baseDamage;
        public int BaseSpeed => baseSpeed;
    }
}