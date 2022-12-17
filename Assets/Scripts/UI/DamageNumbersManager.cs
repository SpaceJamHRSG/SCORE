using System;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class DamageNumbersManager : MonoBehaviour
    {
        [SerializeField] private DamageNumberUI damageNumberPrefab;

        private void OnEnable()
        {
            HealthEntity.OnTakeHit += RespondToHit;
        }

        private void OnDisable()
        {
            HealthEntity.OnTakeHit -= RespondToHit;
        }

        private void RespondToHit(int dmg, HealthEntity entity)
        {
            SpawnDamageNumber(damageNumberPrefab, entity.transform.position, dmg);
        }
        private void SpawnDamageNumber(DamageNumberUI damageNum, Vector3 location, int value)
        {
            DamageNumberUI ui = Instantiate(damageNum, location, Quaternion.identity);
            ui.SetDamageValue(value);
        }
    }
}