using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure;

namespace Code.Human.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private HumanAnimator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        private Vector3 _lookAtPoint;

        private void Start()
        {
            IEnumerable<Vector3> enemyPositions = GameObject.FindGameObjectsWithTag(Tags.Enemy)
                .Select(x => x.transform.position);

            foreach (Vector3 enemyPosition in enemyPositions)
                _lookAtPoint += enemyPosition;

            _lookAtPoint /= enemyPositions.Count();

            SetDestination(
                FindObjectOfType<WayPoint>().transform.position);
        }

        private void Update()
        {
            transform.LookAt(_lookAtPoint);
        }

        private void SetDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _animator.StopShooting();
            _animator.Run();
        }
    }
}