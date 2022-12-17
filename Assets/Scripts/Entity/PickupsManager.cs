using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class PickupsManager : MonoBehaviour
    {
        private List<Pickup> _pickups;
        private PlayerManager _player;

        private void Awake()
        {
            _pickups = new List<Pickup>();
        }

        private void Update()
        {
            
        }
    }
}