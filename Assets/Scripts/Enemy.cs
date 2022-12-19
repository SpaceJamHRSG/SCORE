using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public abstract class Enemy : MonoBehaviour
{

    private const float DISPERSE_CHANCE = 0.1f;
    
    [SerializeField] private float _baseMovementSpeed;

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
    private Vector2 _randomDisperseDirection;
    private float AISeed;
    private bool _disperseOnly;

    protected virtual void Start()
    {
        _randomDisperseDirection = UnityEngine.Random.insideUnitCircle.normalized;
        AISeed = UnityEngine.Random.value;
        _disperseOnly = AISeed < DISPERSE_CHANCE;
    }

    public void SetDirector(EnemyDirector ed) {
        enemyDirector = ed;
    }

    protected void Update() {
        if (!IsActive) return;
        attackCooldown += Time.deltaTime;
        if (playerReference != null && !_disperseOnly)
        {
            _rigidbody.velocity = (playerReference.transform.position - transform.position).normalized * movementSpeed *
                                  _baseMovementSpeed;
        }
        else
        {
            _rigidbody.velocity = (_randomDisperseDirection * movementSpeed *
                                   _baseMovementSpeed * 1.1f);
        }

        int a = 1;
    }

    protected void OnCollisionStay2D(Collision2D collision) {
        if (attackCooldown < (1 / attackRate) || !IsActive) return;
        
        // Damage player
        if (playerReference!= null && collision.gameObject.Equals(playerReference.gameObject)) {
            playerReference.GetComponent<Entity.HealthEntity>().TakeDamage((int)damage);
            attackCooldown = 0;
        }

    }
}
