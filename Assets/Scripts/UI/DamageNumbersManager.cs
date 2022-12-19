using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class DamageNumbersManager : MonoBehaviour
    {
        private const float APPEAR_INTERVAL = 0.05f;
        [SerializeField] private DamageNumberUI damageNumberPrefab;
        [SerializeField] private DamageNumberUI criticalNumberPrefab;

        private Dictionary<HealthEntity, float> _timeTracker;
        private Dictionary<HealthEntity, int> _dmgTracker;
        private Dictionary<HealthEntity, bool> _critTracker;

        private void OnEnable()
        {
            HealthEntity.OnTakeHit += RespondToHit;
            _timeTracker = new Dictionary<HealthEntity, float>();
            _dmgTracker = new Dictionary<HealthEntity, int>();
            _critTracker = new Dictionary<HealthEntity, bool>();
        }

        private void OnDisable()
        {
            HealthEntity.OnTakeHit -= RespondToHit;
        }

        private void Update()
        {
            foreach (var e in _timeTracker.Keys.ToArray())
            {
                if (e == null || !e.isActiveAndEnabled)
                {
                    _timeTracker.Remove(e);
                    _dmgTracker.Remove(e);
                    _critTracker.Remove(e);
                }
            }
            foreach (var e in _timeTracker.Keys.ToArray())
            {
                if (_timeTracker[e] > APPEAR_INTERVAL && _dmgTracker[e] > 0)
                {
                    DamageNumberUI specialUI = e.DamageNumberUI;
                    if (specialUI != null)
                    {
                        SpawnDamageNumber(specialUI, e.transform.position, _dmgTracker[e]);
                        return;
                    }

                    SpawnDamageNumber(_critTracker[e] ? criticalNumberPrefab : damageNumberPrefab, e.transform.position,
                        _dmgTracker[e]);
                    
                    _timeTracker[e] = 0;
                    _dmgTracker[e] = 0;
                    _critTracker[e] = false;
                }
                else
                {
                    _timeTracker[e] += Time.deltaTime;
                }
            }
        }

        private void RespondToHit(int dmg, bool isCritical, HealthEntity entity, GameObject impactParticles)
        {
            _timeTracker[entity] = 0;
            if (!_dmgTracker.ContainsKey(entity)) _dmgTracker[entity] = 0;
            _dmgTracker[entity] += dmg;
            if (!_critTracker.ContainsKey(entity)) _critTracker[entity] = false;
            _critTracker[entity] |= isCritical;
        }
        private void SpawnDamageNumber(DamageNumberUI damageNum, Vector3 location, int value)
        {
            DamageNumberUI ui = Instantiate(damageNum, location, Quaternion.identity);
            ui.SetDamageValue(value);
        }
    }
}