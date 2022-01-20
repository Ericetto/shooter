using UnityEngine;
using UnityEngine.AI;
using System;
using Code.Level.Path;
using Code.Level.Path.Follower;

namespace Code.Human.Hero
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HeroPathFollower : MonoBehaviour, IPathFollower
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _destinationDistance;

        private Vector3 _lookAtPoint;

        public event Action PointReached;

        private void Update()
        {
            transform.LookAt(_lookAtPoint);

            if (_navMeshAgent.hasPath &&
                _navMeshAgent.remainingDistance < _destinationDistance)
            {
                _navMeshAgent.ResetPath();
                PointReached?.Invoke();
            }
        }

        public void SetPoint(IWayPoint wayPoint) => 
            _navMeshAgent.SetDestination(wayPoint.Position);

        public void SetLookTarget(Vector3 point) =>
            _lookAtPoint = point;
    }
}