using System.Collections;
using System.Collections.Generic;
using Entity;
using Game;
using UnityEngine;
using Entity;

public class PlayerManager : MonoBehaviour {
    public string playerName = "default"; // TODO: choose name at start of play(?), for leaderboard

    [SerializeField] private PlayerStats originalStats;

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
    private float damageMultiplier = 1;

    private System.Random random;

    public int BaseMaxHealth
    {
        get => baseMaxHealth;
        set => baseMaxHealth = value;
    }
    
    public float BaseMoveSpeed
    {
        get => baseMovementSpeed;
        set => baseMovementSpeed = value;
    }
    
    public float BaseDamage
    {
        get => damageMultiplier;
        set => damageMultiplier = value;
    }


    // Player inventory
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    // Derived stats
    // TODO: add up the bonuses
    // private int totalAttack;
    private int currentHealth;
    private int currentExperience;


    private Rigidbody2D rigidbody;
    private BoxCollider2D _collider;


    private void OnEnable() {
        HealthEntity.OnDeath += OnDeath;
    }

    private void OnDeath(int dmg, HealthEntity entity) {
        if (entity.gameObject.Equals(this.gameObject)) {
            rigidbody.simulated = false;
            GameManager.Instance.EndGame();
        }
    }

    void Start() {

        ResetStats();
        random = new System.Random();
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

    public bool IsWeaponEnabled(WeaponDefinition wep)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponDefinition == wep)
            {
                return weapon.HasThisWeapon;
            }
        }

        return false;
    }

    public void GrantWeaponLevel(WeaponDefinition wep, int lvl)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponDefinition == wep)
            {
                weapon.GrantWeaponLevel(lvl);
                return;
            }
        }

        Debug.LogError($"No such weapon of {wep.name} found on player.");
    }
    
    public void LevelUpWeapon(WeaponDefinition wep, int lvl)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponDefinition == wep)
            {
                weapon.LevelUpWeapon(lvl);
                return;
            }
        }
        
        Debug.LogError($"No such weapon of {wep.name} found on player.");
    }
    
    public void RemoveWeapon(WeaponDefinition wep)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponDefinition == wep)
            {
                weapon.RemoveWeapon();
            }
        }
        
        Debug.LogError($"No such weapon of {wep.name} found on player.");
    }

    public WeaponDefinition GetRandomWeapon()
    {
        return weapons[weapons.Count].weaponDefinition;
    }
    
    public WeaponDefinition GetWeaponIndex(int i)
    {
        return weapons[i].weaponDefinition;
    }

    public int GetWeaponLevel(WeaponDefinition wep)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponDefinition == wep)
            {
                return weapon.CurrentLevel;
            }
        }
        
        Debug.LogError($"No such weapon of {wep.name} found on player.");
        return 0;
    }

    public void ResetStats()
    {
        BaseMaxHealth = originalStats.MaxHealth;
        BaseDamage = originalStats.BaseDamage;
        BaseMoveSpeed = originalStats.BaseSpeed;
    }

}
