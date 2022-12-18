using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public abstract class Enemy : MonoBehaviour {

    public static bool IsActive;

    protected EnemyDirector enemyDirector; // Controlling director

    protected PlayerManager playerReference; // Player to focus on

    protected float movementSpeed;
    public float MovementSpeed {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    [SerializeField] protected HealthBarUI healthBar;

    protected float damage;
    protected float attackRate;
    protected float attackCooldown;

    protected Rigidbody2D _rigidbody;

    public void SetDirector(EnemyDirector ed) {
        enemyDirector = ed;
    }

    protected void FixedUpdate() {
        attackCooldown += Time.deltaTime;
        if (!IsActive) return;
        _rigidbody.MovePosition(Vector2.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed));
    }

    protected void OnCollisionStay2D(Collision2D collision) {
        if (attackCooldown < (1 / attackRate) || !IsActive) return;
        
        // Damage player
        if (collision.gameObject.Equals(playerReference.gameObject)) {
            playerReference.GetComponent<Entity.HealthEntity>().TakeDamage((int)damage);
            attackCooldown = 0;
        }

    }
}
