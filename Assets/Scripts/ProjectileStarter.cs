using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class ProjectileStarter : MonoBehaviour {
    public EnemyDirector enemyDirector;

    private GameObject target;

    private float movementSpeed = 0.01f;

    private float checkCooldown = 0f;
    private Rigidbody2D rigidbody;

    void Start() {
        enemyDirector = FindObjectOfType<EnemyDirector>();
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine("Logic"); 

    }

    IEnumerator Logic() {
        while (!target) {
            target = enemyDirector.FindClosestEnemy(this.transform.position);
            yield return null;
        }
        this.transform.LookAt(target.transform);
        rigidbody.AddForce(Vector3.forward * 0.001f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // Damage enemy
        if (collision.gameObject.GetComponent<EnemyGruntController>()) {
            collision.gameObject.GetComponent<Entity.HealthEntity>().TakeDamage(Random.Range(8,11)); // TODO: adjust
            Destroy(this.gameObject);
        } else if (collision.gameObject.GetComponent<PlayerManager>()) {
            return;
        }
    }

    IEnumerator DestroyAfterSeconds(int delay) {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
