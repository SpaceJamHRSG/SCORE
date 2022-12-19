using System;
using System.Collections;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileDieOnStill : MonoBehaviour
    {
        private Projectile _projectile;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float startDespawnAfter;
        [SerializeField] private float despawnDuration;

        private float _t1;

        private void Awake()
        {
            _t1 = Time.time;
            _projectile = GetComponent<Projectile>();
        }

        private void Update()
        {
            if (Time.time - _t1 > startDespawnAfter)
            {
                StartCoroutine(DespawnSequence(despawnDuration));
            }
        }

        IEnumerator DespawnSequence(float dur)
        {
            float t = 0;
            while (t < dur)
            {
                t += Time.deltaTime;
                Color spriteColor = sprite.color;
                spriteColor.a = 1 - t / dur;
                sprite.color = spriteColor;
                yield return null;
            }
            _projectile.Despawn();
        }
    }
}