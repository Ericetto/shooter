using UnityEngine;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;
using Code.Logic.AnimatorState;

namespace Code.Human.Hero
{
    public class HeroShooting : HumanAttack
    {
        [SerializeField] private LayerMask _castLayerMask;

        private Camera _camera;

        private IInputService _inputService;

        private bool _gunCanShoot;
        
        private void Start()
        {
            _camera = Camera.main;
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (_inputService.IsShootButtonDown() || _inputService.IsShootButton())
                Shoot();
        }

        private void Shoot()
        {
            _animator.Shooting();

            if (_animator.IsInTransition)
                _animator.StateExited += UnlockGun;
        }

        private void UnlockGun(AnimatorState _)
        {
            _animator.StateExited -= UnlockGun;
            _gunCanShoot = true;
        }

        private void LockGun() => _gunCanShoot = false;

        private void RotateGunToTarget()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out var hitInfo, 300, _castLayerMask))
                targetPoint = hitInfo.point;
            else
                targetPoint = ray.GetPoint(300);

            var shootDirection = targetPoint - _gun.transform.position;
            shootDirection.Normalize();

            _gun.transform.forward = shootDirection;
        }

        // Call from Gun Shooting animation clip
        protected override void GunShoot()
        {
            if (!_gunCanShoot)
                return;

            RotateGunToTarget();
            _gun.Shoot();
        }
    }
}