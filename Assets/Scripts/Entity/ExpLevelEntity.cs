using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class ExpLevelEntity : MonoBehaviour
    {
        public static event Action<int, ExpLevelEntity> OnLevelUp;
        
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _experience;
        [SerializeField] private int _level;
        [SerializeField] private AnimationCurve expCurve;
        private Dictionary<int, int> _expRequiredToLevel;

        public int Level
        {
            get => _level;
            set => _level = value;
        }
        
        public int Exp
        {
            get => _experience;
            set => _experience = value;
        }

        private void Awake()
        {
            _expRequiredToLevel = new Dictionary<int, int>();
            for (int i = 0; i < _maxLevel; i++)
            {
                _expRequiredToLevel.Add(i, (int)expCurve.Evaluate(i));
            }

            _experience = 0;
            _level = 1;
        }

        public void GainExperience(int exp)
        {
            _experience += exp;
            if (_level >= _maxLevel) return;
            int expToNext = _expRequiredToLevel[_level + 1];
            while (_experience >= expToNext && _level < _maxLevel)
            {
                _experience -= expToNext;
                LevelUp(1);
                expToNext = _expRequiredToLevel[_level + 1];
            }
        }

        public void LevelUp(int by)
        {
            _level += by;
            OnLevelUp?.Invoke(_level, this);
        }

        public int ExpRequiredToNext()
        {
            if (_level == _maxLevel) return 0;
            return (int)expCurve.Evaluate(_level + 1);
        }
    }
}