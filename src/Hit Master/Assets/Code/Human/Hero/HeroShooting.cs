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
            _isActive = true;
        }

        private IEnumerator Start()
        {
            while (_inputService == null)
                yield return null;

            while (true)
            {
                if (_isActive)
                {
                    if (_inputService.IsShootButtonDown() || _inputService.IsShootButton())
                    {
                        while (_animator.IsInTransition)
                            yield return null;

                        SetTarget();
                        PullTrigger();
                    }

                    if (_inputService.IsShootButtonUp())
                        PullUpTrigger();
                }

                yield return null;
            }
        }

        protected override void PullTrigger()
        {
            if (_animator.IsInTransition)
                return;

            _equipment.Gun.transform.LookAt(_targetPoint);

            if (_equipment.Gun.PullTrigger())
                _animator.Shoot();
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