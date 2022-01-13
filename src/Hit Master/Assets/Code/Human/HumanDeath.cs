using UnityEngine;
using System;

namespace Code.Human
{
    [RequireComponent(typeof(HumanHealth), typeof(HumanAnimator))]
    public class HumanDeath : MonoBehaviour
    {
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private HumanBody _body;
        [SerializeField] private HumanEquipment _equipment;
        [SerializeField] private HumanHealth _health;

        private HumanShooting _shooting;

        public event Action Happened;

        private void Start()
        {
            _shooting = GetComponent<HumanShooting>();
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        protected virtual void Die()
        {
            _health.HealthChanged -= OnHealthChanged;

            _equipment.DropGun();
            _shooting.enabled = false;
            _animator.enabled = false;


            _body.EnablePhysics();

            Happened?.Invoke();
        }
    }
}