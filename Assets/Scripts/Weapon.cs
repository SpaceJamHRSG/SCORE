using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game;
using Projectiles;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float damageMultiplier;
    public WeaponDefinition weaponDefinition;

    private ProjectileMultiShooter _activeShooter;
    private int _currentLevel;

    public bool IsStartingWeapon;
    private bool _weaponEnabled;

    public bool HasThisWeapon => _weaponEnabled;
    public int CurrentLevel => _currentLevel;
    
    public void GrantWeaponLevel(int level, PlayerManager player)
    {
        if (level == _currentLevel) return;
        ProjectileMultiShooter newWeapon = weaponDefinition.GetShooter(level);
        if (newWeapon == null)
        {
            Debug.LogWarning($"No weapon of level {level} exists");
            return;
        }

        if(_activeShooter != null)
            Destroy(_activeShooter.gameObject);
        _weaponEnabled = true;
        _activeShooter = Instantiate(newWeapon, transform);
        _currentLevel = level;
        _activeShooter.SetPart(weaponDefinition.LineName);
        _activeShooter.SetProjectile(weaponDefinition.GetWeaponInfoLevel(level).Projectile);
        _activeShooter.AssignDamageStats(player.Damage, 1, 0, 1);
        newWeapon.transform.position = Vector3.zero;
        newWeapon.transform.rotation = Quaternion.identity;
    }

    public void LevelUpWeapon(int by = 1, PlayerManager player = null)
    {
        GrantWeaponLevel(_currentLevel + by, player);
    }

    public void RemoveWeapon()
    {
        Destroy(_activeShooter);
        _currentLevel = 0;
        _activeShooter = null;
        _weaponEnabled = false;
    }
}