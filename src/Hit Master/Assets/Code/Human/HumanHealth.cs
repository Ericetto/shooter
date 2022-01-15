using UnityEngine;
using System;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator))]
    public class HumanHealth : MonoBehaviour
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

        public void TakeDamage(float damage)
        {
            _current -= damage;
            HealthChanged?.Invoke();
        }
    }
}