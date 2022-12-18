using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Game;
using UnityEngine;
using Entity;
using Random = System.Random;

[RequireComponent(typeof(ExpLevelEntity), typeof(HealthEntity))]
public class PlayerManager : MonoBehaviour
{
    public bool IsActive;
    
    private ExpLevelEntity _expLevelEntity;
    private HealthEntity _healthEntity;
    
    private Collider2D[] _collidersArray;
    private Pickup[] _pickupsArray;
    
    public string playerName = "default"; // TODO: choose name at start of play(?), for leaderboard

    [SerializeField] private PlayerStats originalStats;

    // Progress statistics
    private float survivalTime = 0; // Time spent alive, in seconds
    //private float gruntsDefeated = 0; // Enemies killed
    //private float bossesDefeated = 0;
    //private int baseCriticalChance = 10; // %
    //private int baseCriticalDamageBonus = 50; // %
    private float baseMovementSpeed = 5f;
    public float pickupRadius = 3f;
    private float damageMultiplier = 1;

    private System.Random random;

    public int MaxHealth
    {
        get => _healthEntity.GetMaxHealth();
        set => _healthEntity.SetHealth(value);
    }
    
    public float MoveSpeed
    {
        get => baseMovementSpeed;
        set => baseMovementSpeed = value;
    }
    
    public float Damage
    {
        get => damageMultiplier;
        set => damageMultiplier = value;
    }

    public int Exp
    {
        get => _expLevelEntity.Exp;
        set => _expLevelEntity.Exp = value;
    }

    public int Health
    {
        get => _healthEntity.GetHealth();
        set => _healthEntity.SetHealth(value);
    }

    public void Heal(int h)
    {
        _healthEntity.Heal(h);
    }

    public int ExpToNext => _expLevelEntity.ExpRequiredToNext();

    public int Level
    {
        get => _expLevelEntity.Level;
        set => _expLevelEntity.Level = 1;
    }


    // Player inventory
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    private Rigidbody2D rigidbody;
    private BoxCollider2D _collider;


    private void OnEnable() {
        HealthEntity.OnDeath += OnDeath;
        HealthEntity.OnTakeHit += OnTakeHit;
    }
    private void OnDisable() {
        HealthEntity.OnDeath -= OnDeath;
        HealthEntity.OnTakeHit -= OnTakeHit;
    }
    
    private void OnTakeHit(int dmg, bool crit, HealthEntity entity, Sprite impactParticles) {
        if (entity.gameObject.Equals(this.gameObject)) {
            rigidbody.simulated = false;
            GameManager.Instance.EndGame();
        }
    }

    private void OnDeath(int dmg, HealthEntity entity, Sprite impactParticles) {
        if (entity.gameObject.Equals(this.gameObject)) {
            rigidbody.simulated = false;
            GameManager.Instance.EndGame();
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _expLevelEntity = GetComponent<ExpLevelEntity>();
        _healthEntity = GetComponent<HealthEntity>();
    }

    void Start()
    {
        _collidersArray = new Collider2D[512];
        _pickupsArray = new Pickup[512];
        
        ResetStats();
        random = new System.Random();
        weapons.Clear();
        foreach (Transform t in transform)
        {
            Weapon w = t.GetComponent<Weapon>();
            if(w != null) weapons.Add(w);
        }
        foreach (var weapon in weapons)
        {
            if(weapon.IsStartingWeapon) weapon.GrantWeaponLevel(1, this);
        }

        // Get the Rigidbody and Box Collider references
        
    }

    void Update()
    {
        if (!IsActive)
        {
            rigidbody.velocity = Vector2.zero;
            return;
        }

        survivalTime += Time.deltaTime;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector2(horizontalInput * baseMovementSpeed, verticalInput * baseMovementSpeed);
        
        Physics2D.OverlapCircleNonAlloc(transform.position, pickupRadius, _collidersArray);
        foreach (var col in _collidersArray)
        {
            if (col == null) continue;
            Pickup pk = col.gameObject.GetComponent<Pickup>();
            if (pk == null) continue;
            pk.AttractTo(transform);
        }
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
                weapon.GrantWeaponLevel(lvl, this);
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
        Random random = new Random();
        return weapons[random.Next(weapons.Count)].weaponDefinition;
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
        MaxHealth = originalStats.MaxHealth;
        Damage = originalStats.BaseDamage;
        MoveSpeed = originalStats.BaseSpeed;
        Health = MaxHealth;
        Exp = 0;
        Level = 1;
    }

    public int GetWeaponLevelOf(string s)
    {
        foreach(Weapon w in weapons)
        {
            if (w.weaponDefinition.LineName.Equals(s)) return w.CurrentLevel;
        }
        Debug.LogWarning($"No weapon corresponding to line {s} found!");
        return 0;
    }

}
