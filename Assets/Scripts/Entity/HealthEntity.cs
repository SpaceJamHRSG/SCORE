using System;
using UnityEngine;

namespace Entity
{
    public class HealthEntity : MonoBehaviour
    {
        public static event Action<int, HealthEntity> OnTakeHit;
        public static event Action<int, HealthEntity> OnDeath;
        public static event Action<int, HealthEntity> OnHeal;

        public event Action<int> OnThisDeath;

        [SerializeField] private int maxHealth;
        private int _health;

        [SerializeField] private Allegiance allegiance;
        public Allegiance Allegiance => allegiance;

        private void OnEnable()
        {
            _health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            OnTakeHit?.Invoke(damage, this);
            if (_health <= 0)
            {
                OnDeath?.Invoke(damage, this);
                OnThisDeath?.Invoke(damage);
            }
        }

        public void Heal(int h)
        {
            _health += h;
            OnHeal?.Invoke(h, this);
        }

        public int GetHealth() {
            return _health;
        }

        public int GetMaxHealth() {
            return maxHealth;
        }
    }
}