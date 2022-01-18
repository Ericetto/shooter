using UnityEngine;
using System;
using Code.Human.Mediator;

namespace Code.Human
{
    internal class HumanHealth : HumanComponent
    {
        [SerializeField] protected float _current;
        [SerializeField] protected float _max;
        
        public bool IsAlive => _current > 0;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public event Action HealthChanged;
        public event Action Died;

        public void TakeDamage(float damage)
        {
            if (!IsAlive)
                return;

            _current -= damage;

            HealthChanged?.Invoke();

            if (_current <= 0)
                Died?.Invoke();
        }
    }
}