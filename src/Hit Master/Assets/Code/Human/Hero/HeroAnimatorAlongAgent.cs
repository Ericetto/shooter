using UnityEngine;
using UnityEngine.AI;

namespace Code.Human.Hero
{
    [RequireComponent(typeof(NavMeshAgent), typeof(HumanAnimator))]
    public class HeroAnimatorAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HumanAnimator _animator;

        private const float VelocityThreshold = 0.1f;

        private void Update()
        {
            if (ShouldMove())
                Move();
            else
                Stop();
        }

        private void Move()
        {
            _animator.StopShooting();
            _animator.Run();
        }

        private void Stop()
        {
            _animator.StartShooting();
            _animator.Stop();
        }

        private bool ShouldMove()
        {
            return _agent.velocity.magnitude > VelocityThreshold &&
                   _agent.remainingDistance > _agent.radius;
        }
    }
}