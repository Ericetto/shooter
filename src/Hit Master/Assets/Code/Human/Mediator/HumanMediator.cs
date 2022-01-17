using UnityEngine;
using System;

namespace Code.Human.Mediator
{
    [RequireComponent(typeof(HumanAnimator), typeof(HumanBody), typeof(HumanEquipment))]
    [RequireComponent(typeof(HumanHealth), typeof(HumanShooting))]
    public class HumanMediator : MonoBehaviour
    {
        private HumanAnimator _animator;
        private HumanBody _body;
        private HumanEquipment _equipment;
        private HumanHealth _health;
        private HumanShooting _shooting;
        
        public event Action AnimatorStateExited;

        public bool AnimatorIsInTransition => _animator.IsInTransition;
        public bool IsPistolEquipped => _equipment.Gun.IsPistol;

        private void Awake()
        {
            _animator = GetComponent<HumanAnimator>();
            _body = GetComponent<HumanBody>();
            _equipment = GetComponent<HumanEquipment>();
            _health = GetComponent<HumanHealth>();
            _shooting = GetComponent<HumanShooting>();

            _health.Died += OnDied;
            _animator.StateExited += OnAnimatorStateExited;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnDied;
            _animator.StateExited -= OnAnimatorStateExited;
        }

        public void TakeDamage(float damage) => _health.TakeDamage(damage);

        public void SetActiveAnimator(bool value) => _animator.SetActive(value);
        public void OnAnimatorStateExited(AnimatorState.AnimatorState _) => AnimatorStateExited?.Invoke();
        public void StartShooting() => _animator.StartShooting();
        public void AnimatorShoot() => _animator.Shoot();

        public void SetGunType(bool isPistol) => _animator.SetGunType(isPistol);
        public void SetGunTarget(Vector3 targetPoint) => _equipment.Gun.LookAt(targetPoint);
        public bool PullTrigger() => _equipment.Gun.PullTrigger();
        public void PullUpTrigger() => _equipment.Gun.PullUpTrigger();

        public void AnimatorMove()
        {
            _animator.StopShooting();
            _animator.Run();
        }

        public void AnimatorStopMove()
        {
            _animator.StartShooting();
            _animator.Stop();
        }


        private void OnDied()
        {
            _health.HealthChanged -= OnDied;

            _equipment.DropGun();
            _shooting.enabled = false;
            _animator.enabled = false;

            _body.EnablePhysics();
        }

    }
}