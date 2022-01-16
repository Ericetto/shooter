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
        private bool _isShooting;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        private void Update()
        {
            UpdateCooldown();

            if (!IsEnable)
                return;

            LookAtHero();

            if (!_isShooting && _currentAttackCooldown <= 0)
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
            _isShooting = true;
            _animator.StartShooting();
            StartCoroutine(GunShooting());
        }

        private IEnumerator GunShooting()
        {
            WaitForSeconds wait = new WaitForSeconds(0.25f);

            yield return TakeAim(wait);

            int shootCount = _equipment.Gun.IsPistol ? 1 : 5;

            for (int i = 0; i < shootCount; i++)
            {
                PullTrigger();
                yield return wait;
                PullUpTrigger();
            }

            _currentAttackCooldown = _attackCooldown;
            _isShooting = false;
        }

        private IEnumerator TakeAim(WaitForSeconds wait)
        {
            yield return wait;
            while (_animator.IsInTransition)
                yield return wait;
        }
    }
}