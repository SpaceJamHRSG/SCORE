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
        //StartCoroutine("DestroyAfterSeconds", 8);

    }

    IEnumerator Logic() {
        int i = 0;

        while (!target) {
            target = enemyDirector.FindClosestEnemy(this.transform.position);
            yield return null;
        }
        while (target) {
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            switch (i) {
                case 0:
                    rigidbody.AddForce(Vector2.left * 1f);
                    i++;
                    break;
                case 1:
                    rigidbody.AddForce(Vector2.right * 1f);
                    i--;
                    break;
            } 
            yield return null;
        }
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
