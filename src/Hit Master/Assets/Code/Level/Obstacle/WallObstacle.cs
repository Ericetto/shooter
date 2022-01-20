using UnityEngine;
using System.Collections;

namespace Code.Level.Obstacle
{
    public class WallObstacle : ObstacleBase
    {
        [SerializeField] private Rigidbody _checkRigidbody;

        private Vector3 _checkRigidbodyStartPosition;

        public override bool IsDestroyed => CheckDistance();

        public override void Init()
        {
            _checkRigidbodyStartPosition = _checkRigidbody.position;
            LookAtPoint = _checkRigidbody.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(CheckWallCondition());
        }

        private IEnumerator CheckWallCondition()
        {
            WaitForSeconds wait = new WaitForSeconds(0.5f);

            while (_checkRigidbody.velocity.sqrMagnitude > 10)
                yield return wait;

            if (CheckDistance())
                OnOvercame();
        }

        private bool CheckDistance() => Vector3.Distance(
            _checkRigidbody.position, _checkRigidbodyStartPosition) > 5;
    }
}