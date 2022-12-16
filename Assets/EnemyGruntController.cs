using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGruntController : MonoBehaviour {

    private PlayerManager playerReference;

    private float movementSpeed = 0.01f;

    // Start is called before the first frame update
    void Start() {
        playerReference = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        this.transform.position = Vector2.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed);
    }
}
