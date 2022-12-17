using System;
using UnityEngine;

namespace Entity
{
    public class HealthEntity : MonoBehaviour
    {
        public Action<int> OnTakeHit;
        public Action<int> OnDeath;
        public Action<int> OnHeal;

        [SerializeField] private int maxHealth;
        private int _health;

        private void OnEnable()
        {
            _health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            OnTakeHit?.Invoke(damage);
            if (_health <= 0)
            {
                OnDeath?.Invoke(damage);
            }
        }

        public void Heal(int h)
        {
            _health += h;
            OnHeal?.Invoke(h);
        }
    }
}