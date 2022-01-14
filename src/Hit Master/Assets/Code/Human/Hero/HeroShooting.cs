using UnityEngine;
using System.Collections;
using Code.Infrastructure.Services.Input;

namespace Code.Human.Hero
{
    public class HeroShooting : HumanShooting
    {
        [SerializeField] private LayerMask _inputCastLayerMask;

        private const float RaycastDistance = 300;

        private IInputService _inputService;
        private Camera _camera;
        private Vector3 _targetPoint;

        public void Construct(IInputService inputService)
        {
            _camera = Camera.main;
            _inputService = inputService;
        }

        private IEnumerator Start()
        {
            while (_inputService == null)
                yield return null;

            while (true)
            {
                if (_inputService.IsShootButtonDown() || _inputService.IsShootButton())
                {
                    while (_animator.IsInTransition)
                        yield return null;

                    SetTarget();
                    Shoot();
                }

                yield return null;
            }
        }

        protected override void GunShoot()
        {
            Gun.transform.LookAt(_targetPoint);
            Gun.Shoot();
        }

        private void SetTarget()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            _targetPoint = Raycast(ray, out RaycastHit hitInfo) ?
                hitInfo.point : ray.GetPoint(RaycastDistance);
        }

        private bool Raycast(Ray ray, out RaycastHit hitInfo) =>
            Physics.Raycast(ray, out hitInfo, RaycastDistance, _inputCastLayerMask);
    }
}