using UnityEngine;
using UnityEngine.AI;
using Code.Human.Mediator;

namespace Code.Human.Hero
{
    [RequireComponent(typeof(NavMeshAgent))]
    internal class HeroAnimatorAlongAgent : HumanComponent
    {
        private NavMeshAgent _agent;

        private const float VelocityThreshold = 0.1f;

        protected override void OnAwake() => _agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if (ShouldMove())
                Mediator.AnimatorMove();
            else
                Mediator.AnimatorStopMove();
        }

        private bool ShouldMove() =>
            _agent.velocity.magnitude > VelocityThreshold &&
            _agent.remainingDistance > _agent.radius;
    }
}