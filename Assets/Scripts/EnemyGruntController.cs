using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class EnemyGruntController : Enemy {

    void Start() {

        Debug.Assert(enemyDirector);
        playerReference = FindObjectOfType<PlayerManager>();
        _rigidbody = GetComponent<Rigidbody2D>();

        movementSpeed = 0.03f;
        damage = 1;
        attackRate = 0.5f;
        attackCooldown = 0f;
    }

    private void OnEnable() {
        attackCooldown = 1 / attackRate;

        // Death event
        HealthEntity.OnDeath += (dmg, entity, s) => {
            if (entity == null || this == null) return;
            if (entity.gameObject.Equals(this.gameObject)) {

                GameManager.Instance.GruntsDefeated += 1;

                enemyDirector.RemoveEnemy(this.gameObject);
                GetComponent<Collider2D>().enabled = false;
                healthBar.gameObject.SetActive(false);
            }
        };

    }
}
