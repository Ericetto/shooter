using System.Collections.Generic;
using System.Linq;
using Code.Level.Obstacle;
using Code.Level.Path.Follower;
using UnityEngine;

namespace Code.Level.Path.StateMachine.States
{
    public class RunToNexPointState : IPathState
    {
        private readonly IPathStateMachine _stateMachine;
        private readonly IPathFollower _heroFollower;
        private readonly IPathShooting _heroShooting;
        private readonly Dictionary<int, IWayPoint> _points;

        private IWayPoint _currentWayPoint;

        public RunToNexPointState(IPathStateMachine stateMachine,
            IPathFollower heroFollower,
            IPathShooting heroShooting,
            IWayPoint[] points)
        {
            _stateMachine = stateMachine;
            _heroFollower = heroFollower;
            _heroShooting = heroShooting;
            _points = InitUniquePoints(points);
        }

        public void Enter()
        {
            _heroShooting.Enable();

            _currentWayPoint ??= _points[0];

            if (!_points.ContainsKey(_currentWayPoint.Id + 1))
            {
                _stateMachine.OnCompleted();
                return;
            }

            _currentWayPoint = _points[_currentWayPoint.Id + 1];

            _heroFollower.SetPoint(_currentWayPoint);
            _heroFollower.SetLookTarget(_currentWayPoint.LookAtPoint);
            _heroFollower.PointReached += OnPointReached;
        }

        public void Exit()
        {
            _heroShooting.Disable();
        }

        private void OnPointReached()
        {
            _heroFollower.PointReached -= OnPointReached;

            _stateMachine.Enter<ObstacleOvercomingState, IObstacle>(
                _currentWayPoint.Obstacle);
        }

        private Dictionary<int, IWayPoint> InitUniquePoints(IWayPoint[] points)
        {
            Validate(points);

            var uniquePoints = new Dictionary<int, IWayPoint>(points.Length);

            foreach (var wayPoint in points)
                uniquePoints.Add(wayPoint.Id, wayPoint);
            
            return uniquePoints;
        }

        private void Validate(IWayPoint[] points)
        {
            int startPointCount = points
                .Count(x => x.Type == WayPointType.Start);

            int finishPointCount = points
                .Count(x => x.Type == WayPointType.Finish);

            if (startPointCount != 1)
                Debug.LogError(
                    $"The start way point must be the only one, but now {startPointCount}!");

            if (finishPointCount != 1)
                Debug.LogError(
                    $"The finish way point must be the only one, but now {finishPointCount}!");
        }
    }
}