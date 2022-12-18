using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity
{
    public class HealthEntity : MonoBehaviour
    {
        public static event Action<int, bool, HealthEntity, Sprite> OnTakeHit;
        public static event Action<int, HealthEntity, Sprite> OnDeath;
        public static event Action<int, HealthEntity> OnHeal;

        public event Action<int> OnThisDeath;
        public event Action<int, bool, Sprite> OnThisHit;

        [SerializeField] private int maxHealth;
        private int _health;

        [SerializeField] private Allegiance allegiance;
        [Space] [SerializeField] private DamageNumberUI damageNumberUI;
        public Allegiance Allegiance => allegiance;
        public DamageNumberUI DamageNumberUI => damageNumberUI;
        private bool _isDead;

        private void OnEnable()
        {
            _isDead = false;
            _health = maxHealth;
        }
        public void TakeDamage(int damage, bool critical = false, Sprite impactParticles = null)
        {
            if (_isDead) return;
            _health -= damage;
            OnTakeHit?.Invoke(damage, critical, this, impactParticles);
            OnThisHit?.Invoke(damage, critical, impactParticles);
            if (_health <= 0)
            {
                _isDead = true;
                BeginDeathSequence();
                OnDeath?.Invoke(damage, this, impactParticles);
                OnThisDeath?.Invoke(damage);
            }
        }

        public void Heal(int h)
        {
            _health += h;
            _health = Math.Min(_health, maxHealth);
            OnHeal?.Invoke(h, this);
        }

        public int GetHealth() {
            return _health;
        }

        public int GetMaxHealth() {
            return maxHealth;
        }

        public void SetHealth(int h)
        {
            _health = h;
        }

        public void BeginDeathSequence()
        {
            StartCoroutine(DeathRoutine(1.5f));
        }

        private IEnumerator DeathRoutine(float time)
        {
            float t = 0;
            while (t < 1)
            {
                transform.localScale = new Vector3(1-t, 1-t, 1-t);
                transform.localRotation = Quaternion.Euler(0,0,t*360);
                t += Time.deltaTime / time;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}