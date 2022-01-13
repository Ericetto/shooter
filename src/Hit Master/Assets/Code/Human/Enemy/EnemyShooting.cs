using UnityEngine;
using System.Collections;
using Code.Weapon;

namespace Code.Human.Enemy
{
    [RequireComponent(typeof(HumanAnimator))]
    public class EnemyShooting : HumanAttack
    {
        [SerializeField] private float _attackCooldown = 3f;
        private float _currentAttackCooldown;
        
        private Transform _heroTransform;
        private bool _isAttacking;

        public void Construct(Gun gun, Transform heroTransform)
        {
            base.EquipWeapon(gun);
            _heroTransform = heroTransform;
        }

        private void Update()
        {
            UpdateCooldown();

            if (!_isActive)
                return;

            LookAtHero();

            if (!_isAttacking && _currentAttackCooldown <= 0)
                StartShooting();
        }

        private void LookAtHero()
        {
            if (_heroTransform)
                transform.LookAt(_heroTransform);
        }

        private void UpdateCooldown()
        {
            if (_currentAttackCooldown > 0)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private void StartShooting()
        {
            _animator.Shooting();
            StartCoroutine(GunShooting());
            _isAttacking = true;
        }

        private IEnumerator GunShooting()
        {
            WaitForSeconds wait = new WaitForSeconds(0.25f);

            for (int i = 0; i < 5; i++)
            {
                _weapon.Attack();
                yield return wait;
            }

            StopShooting();
        }

        private void StopShooting()
        {
            _currentAttackCooldown = _attackCooldown;
            //_animator.StopShooting();
            _isAttacking = false;
        }
    }
}