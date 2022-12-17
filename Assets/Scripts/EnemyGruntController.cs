using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGruntController : MonoBehaviour {

    private PlayerManager playerReference;

    private float health = 10;
    private float movementSpeed = 0.01f;

    private float attackRate = 0.5f;
    private float attackCooldown = 0f;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start() {
        playerReference = FindObjectOfType<PlayerManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        _rigidbody.MovePosition(Vector2.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed));

        attackCooldown += Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (attackCooldown < (1 / attackRate)) return;
        attackCooldown = 0;

        // Damage player
        if (collision.gameObject.Equals(FindObjectOfType<PlayerManager>().gameObject)) {
            playerReference.GetComponent<Entity.HealthEntity>().TakeDamage(5); // TODO: adjust
        }

    }
}
