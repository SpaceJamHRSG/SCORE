using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Game
{
    public class PlayerStatsManager : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private int _level;
        [SerializeField] private int _xp;
        [SerializeField] private List<Weapon> _weapons;
        
        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int Xp
        {
            get => _xp;
            set => _xp = value;
        }

        public List<Weapon> Weapons
        {
            get => _weapons;
            set => _weapons = value;
        }
    }
}