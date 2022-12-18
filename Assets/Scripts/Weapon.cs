using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Game;
using Projectiles;
using UnityEngine;
using Random = System.Random;

public class Weapon : MonoBehaviour
{
    public bool aimWithMouse;
    public float _aimSmoothing;
    public bool autoTargetEnemy;
    public WeaponDefinition weaponDefinition;

    private ProjectileMultiShooter _activeShooter;
    private int _currentLevel;

    public bool IsStartingWeapon;
    private bool _weaponEnabled;

    
    public bool HasThisWeapon => _weaponEnabled;
    public int CurrentLevel => _currentLevel;

    private Camera mainCamera;
    private Transform _target;

    private Quaternion _targetRotation;
    private Quaternion _actualRotation;

    public string LineName => weaponDefinition.LineName;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(_activeShooter != null)
            _activeShooter.AutoTarget = autoTargetEnemy;
        if (aimWithMouse)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            _targetRotation
                = Quaternion.Euler(0,0,180 / Mathf.PI * Mathf.Atan2(-mouseWorldPos.x + transform.position.x, mouseWorldPos.y - transform.position.y));
            _actualRotation = Quaternion.Slerp(_actualRotation, _targetRotation, _aimSmoothing * Time.deltaTime);
            transform.rotation = _actualRotation;
        }

        if (autoTargetEnemy)
        {
            transform.rotation = Quaternion.identity; // outsourced to children (shit solution but oh well game jam)
        }
    }

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