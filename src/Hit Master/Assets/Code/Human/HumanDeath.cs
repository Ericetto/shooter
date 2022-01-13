using UnityEngine;
using System;

namespace Code.Human
{
    [RequireComponent(typeof(HumanHealth), typeof(HumanAnimator))]
    public class HumanDeath : MonoBehaviour
    {
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private HumanBody _body;
        [SerializeField] private HumanAttack _attack;
        [SerializeField] private HumanHealth _health;

        public event Action Happened;

        private void Start() => _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        protected virtual void Die()
        {
            _health.HealthChanged -= OnHealthChanged;

            _attack.DropWeapon();
            _attack.enabled = false;
            _animator.enabled = false;


            _body.EnablePhysics();

            Happened?.Invoke();
        }
    }
}