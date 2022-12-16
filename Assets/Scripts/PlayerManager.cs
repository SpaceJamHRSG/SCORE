using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public string playerName = "default"; // TODO: choose name at start of play(?), for leaderboard

    // Progress statistics
    private float survivalTime = 0; // Time spent alive, in seconds
    //private float gruntsDefeated = 0; // Enemies killed
    //private float bossesDefeated = 0;

    // Player base stats
    //private int baseAttack = 10;
    //private int baseHealth = 100;
    //private int baseCriticalChance = 10; // %
    //private int baseCriticalDamageBonus = 50; // %
    private float baseMovementSpeed = 5f;

    // Player inventory
    // TODO: weapons, upgrades

    // Derived stats
    // TODO: add up the bonuses
    // private int totalAttack;

    private Rigidbody2D rigidbody;
    private BoxCollider2D _collider;

    void Start() {
        // Get the Rigidbody and Box Collider references
        rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update() {

        survivalTime += Time.deltaTime;

        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the character based on the input
        rigidbody.velocity = new Vector2(horizontalInput * baseMovementSpeed, verticalInput * baseMovementSpeed);

    }

}
