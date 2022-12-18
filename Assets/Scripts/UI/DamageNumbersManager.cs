using System;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class DamageNumbersManager : MonoBehaviour
    {
        [SerializeField] private DamageNumberUI damageNumberPrefab;
        [SerializeField] private DamageNumberUI criticalNumberPrefab;

        private void OnEnable()
        {
            HealthEntity.OnTakeHit += RespondToHit;
        }

        private void OnDisable()
        {
            HealthEntity.OnTakeHit -= RespondToHit;
        }

        private void RespondToHit(int dmg, bool isCritical, HealthEntity entity, GameObject impactParticles)
        {
            DamageNumberUI specialUI = entity.DamageNumberUI;
            if (specialUI != null)
            {
                SpawnDamageNumber(specialUI, entity.transform.position, dmg);
                return;
            }

            SpawnDamageNumber(isCritical ? criticalNumberPrefab : damageNumberPrefab, entity.transform.position, dmg);
        }
        private void SpawnDamageNumber(DamageNumberUI damageNum, Vector3 location, int value)
        {
            DamageNumberUI ui = Instantiate(damageNum, location, Quaternion.identity);
            ui.SetDamageValue(value);
        }
    }
}