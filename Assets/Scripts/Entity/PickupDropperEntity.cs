using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Random = System.Random;

namespace Entity
{ 
    [RequireComponent(typeof(HealthEntity))]
    public class PickupDropperEntity : MonoBehaviour
    {
        [Serializable]
        class DropData
        {
            public float Weight;
            public List<Pickup> Drops;
        }

        private HealthEntity _health;
        private Random random;
        [SerializeField] private List<DropData> dropTable;
        [SerializeField] private float dropVariance;

        private void Awake()
        {
            _health = GetComponent<HealthEntity>();
            random = new Random();
        }

        private void OnEnable()
        {
            _health.OnThisDeath += DropSomething;
        }
        
        private void OnDisable()
        {
            _health.OnThisDeath -= DropSomething;
        }

        private void DropSomething(int damageOfLastHit)
        {
            float totalWeight = dropTable.Aggregate<DropData, float>(0, (s,t) => s + t.Weight);
            float seed = (float)random.NextDouble() * totalWeight;
            float x = 0;
            List<Pickup> toDrop = new List<Pickup>();
            foreach (var data in dropTable)
            {
                x += data.Weight;
                if (x >= seed)
                {
                    toDrop = data.Drops;
                    break;
                }
            }

            foreach (var drop in toDrop)
            {
                Pooling.Instance.Spawn<Pickup>(
                    drop.gameObject,
                    (Vector2)transform.position + dropVariance * UnityEngine.Random.insideUnitCircle,
                    transform.rotation
                    );
            }
        }
    }
}