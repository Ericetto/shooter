using UnityEngine;
using Code.Infrastructure.Services.Input;
using Code.Logic.AnimatorState;
using Code.Weapon;

namespace Code.Human.Hero
{
    public class HeroShooting : HumanAttack
    {
        [SerializeField] private LayerMask _inputCastLayerMask;

        private const float RaycastDistance = 300;

        private IInputService _inputService;
        private Camera _camera;
        private Gun _gun;

        private bool _gunCanShoot;

        public void Construct(IInputService inputService, Gun gun)
        {
            _camera = Camera.main;
            _inputService = inputService;
            _gun = gun;

            EquipWeapon(gun);
        }

        private void Update()
        {
            if (_inputService == null)
                return;

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
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            Vector3 targetPoint = Raycast(ray, out RaycastHit hitInfo) ?
                hitInfo.point : ray.GetPoint(RaycastDistance);

            Vector3 shootDirection = targetPoint - _gun.transform.position;
            shootDirection.Normalize();

            _gun.transform.forward = shootDirection;
        }

        private bool Raycast(Ray ray, out RaycastHit hitInfo) =>
            Physics.Raycast(ray, out hitInfo, RaycastDistance, _inputCastLayerMask);
        
        protected override void WeaponAttackByAnimation()
        {
            if (!_gunCanShoot)
                return;

            RotateGunToTarget();
            _gun.Shoot();
        }
    }
}