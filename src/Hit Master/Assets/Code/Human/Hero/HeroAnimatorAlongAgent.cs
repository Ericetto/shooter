using UnityEngine;
using UnityEngine.AI;

namespace Code.Human.Hero
{
    [RequireComponent(typeof(NavMeshAgent), typeof(HumanAnimator))]
    public class HeroAnimatorAlongAgent : MonoBehaviour
    {
        private const float VelocityThreshold = 0.1f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HumanAnimator _animator;

        private void Update()
        {
            if (ShouldMove())
                _animator.Run();
            else
                _animator.Stop();
        }

        private bool ShouldMove()
        {
            return _agent.velocity.magnitude > VelocityThreshold &&
                   _agent.remainingDistance > _agent.radius;
        }
    }
}