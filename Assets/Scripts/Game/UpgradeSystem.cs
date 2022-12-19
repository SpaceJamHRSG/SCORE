using System;
using System.Collections.Generic;
using System.Diagnostics;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Game
{
    public class UpgradeSystem : MonoBehaviour
    {
        public static event Action OnClose;
        
        private Random _random;
        private Stack<IUpgrade> _appliedUpgrades;
        [SerializeField] private List<Stat> _stats;
        private Dictionary<StatType, Stat> _getStatInfo;
        private PlayerManager _activePlayer;
        public Dictionary<StatType, Stat> GetStatInfo => _getStatInfo;
        public UpgradeUI UI;
        public PlayerManager ActivePlayer
        {
            get { return _activePlayer; }
            set { _activePlayer = value; }
        }

        private void Awake()
        {
            _random = new Random();
            _appliedUpgrades = new Stack<IUpgrade>();
        }

        private void Start()
        {
            _getStatInfo = new Dictionary<StatType, Stat>();
            foreach (var s in _stats)
            {
                _getStatInfo[s.Type] = s;
            }
        }

        public IUpgrade GetRandomUpgrade(PlayerManager player) // 1/2 chance of stat upgrade; 1/2 chance of weapon upgrade
        {
            int upgradeTypeID = _random.Next(0, 4);

            StatUpgrade GiveStatUpgrade()
            {
                int statTypeID = _random.Next(0, _stats.Count);
                Stat stat = _stats[statTypeID];
                return new StatUpgrade(stat, stat.BaseUpgradeUnit + stat.BaseUpgradeUnit / 3 * ((float)_random.NextDouble() * 2 - 1));
            }
            
            if (upgradeTypeID < 2)
            {
                return GiveStatUpgrade();
            }

            WeaponDefinition wepToUpgrade = player.GetRandomWeapon();
            if (player.GetWeaponLevel(wepToUpgrade) == wepToUpgrade.MaxLevel)
            {
                return GiveStatUpgrade();
            }
            return new WeaponUpgrade(wepToUpgrade, player.GetWeaponLevel(wepToUpgrade) + 1);
        }
        public void ApplyUpgrade(IUpgrade upgrade, PlayerManager player)
        {
            upgrade.Apply(player);
            _appliedUpgrades.Push(upgrade);
        }

        public void OpenUpgradeScreen(int choices)
        {
            UI.enabled = true;
            UI.gameObject.SetActive(true);
            List<IUpgrade> upgrades = new List<IUpgrade>();
            for (int i = 0; i < choices; i++)
            {
                IUpgrade upgrade = GetRandomUpgrade(ActivePlayer);
                bool sameUpgradeExists = false;
                foreach (var u in upgrades)
                {
                    if (u.GetTypeID().Equals(upgrade.GetTypeID()))
                    {
                        i--;
                        sameUpgradeExists = true;
                    }
                }

                if (sameUpgradeExists) continue;
                upgrades.Add(upgrade);
            }
            UI.SetUpgrades(upgrades);
            UI.Refresh();
        }

        public void InvokeClose()
        {
            OnClose?.Invoke();
        }
    }
}