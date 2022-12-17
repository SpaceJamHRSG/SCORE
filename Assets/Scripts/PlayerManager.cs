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
    private int level = 1;
    //private int baseAttack = 10;
    private int baseMaxHealth = 100;
    //private int baseCriticalChance = 10; // %
    //private int baseCriticalDamageBonus = 50; // %
    private float baseMovementSpeed = 5f;
    private float pickupRadius = 3f;
    

    // Player inventory
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    // Derived stats
    // TODO: add up the bonuses
    // private int totalAttack;
    private int currentHealth;
    private int currentExperience;


    private Rigidbody2D rigidbody;
    private BoxCollider2D _collider;

    void Start() {
        foreach (Transform t in transform)
        {
            Weapon w = t.GetComponent<Weapon>();
            if(w != null) weapons.Add(w);
        }
        foreach (var weapon in weapons)
        {
            if(weapon.IsStartingWeapon) weapon.GrantWeaponLevel(2);
        }

        currentHealth = baseMaxHealth;

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
