using UnityEngine;
using System;
using Code.Weapon;

namespace Code.Human
{
    [RequireComponent(typeof(HumanHealth), typeof(HumanAnimator))]
    public class HumanDeath : MonoBehaviour
    {
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private MonoBehaviour _attack;
        [SerializeField] private HumanBody _body;
        [SerializeField] private Gun _gun;
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

            _attack.enabled = false;
            _animator.enabled = false;

            _body.EnablePhysics();

            DropWeapon();

            Happened?.Invoke();
        }

        protected void DropWeapon()
        {
            _gun.SetActivePhysics(true);
            _gun.AddForce(new Vector3(0, 100, 0));
        }
    }
}