using UnityEngine;
using System.Collections;

namespace Code.Human.Enemy
{
    [RequireComponent(typeof(HumanAnimator))]
    public class EnemyShooting : HumanShooting
    {
        [SerializeField] private float _attackCooldown = 3f;
        private float _currentAttackCooldown;
        
        private Transform _heroTransform;
        private bool _isAttacking;

        public void Construct(Transform heroTransform)
        {
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
            
            StartCoroutine(GunShooting());
        }

        private IEnumerator GunShooting()
        {
            _isAttacking = true;

            WaitForSeconds wait = new WaitForSeconds(0.25f);

            while (_animator.IsInTransition)
                yield return wait;

            int shootCount = Gun.IsPistol ? 1 : 5;

            for (int i = 0; i < shootCount; i++)
            {
                Shoot();
                yield return wait;
            }

            _currentAttackCooldown = _attackCooldown;
            _isAttacking = false;
        }
    }
}