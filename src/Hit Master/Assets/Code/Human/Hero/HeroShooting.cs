using UnityEngine;
using System.Collections;
using Code.Infrastructure.Services.Input;
using Code.Level.Way.Follower;

namespace Code.Human.Hero
{
    public class HeroShooting : HumanShooting, IWayShooting
    {
        [SerializeField] private LayerMask _inputCastLayerMask;

        private const float RaycastDistance = 300;

        private IInputService _inputService;
        private Camera _camera;

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
                yield return null;
                
                if (_inputService.IsShootButtonDown() || _inputService.IsShootButton())
                {
                    if (!IsEnable)
                        continue;

                    while (Mediator.AnimatorIsInTransition)
                        yield return null;

                    SetGunTarget();
                    PullTrigger();
                }

                if (_inputService.IsShootButtonUp())
                    PullUpTrigger();
            }
        }

        private void SetGunTarget()
        {
            if (Mediator.AnimatorIsInTransition)
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            Vector3 targetPoint = Raycast(ray, out RaycastHit hitInfo) ?
                hitInfo.point : ray.GetPoint(RaycastDistance);

            Mediator.SetGunTarget(targetPoint);
        }

        private bool Raycast(Ray ray, out RaycastHit hitInfo) =>
            Physics.Raycast(ray, out hitInfo, RaycastDistance, _inputCastLayerMask);
    }
}