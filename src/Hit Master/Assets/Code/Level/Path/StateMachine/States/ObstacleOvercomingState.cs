using Code.Level.Obstacle;
using Code.Level.Path.Follower;

namespace Code.Level.Path.StateMachine.States
{
    public class ObstacleOvercomingState : IPathPayloadedState<IObstacle>
    {
        private readonly IPathStateMachine _stateMachine;
        private readonly IPathFollower _heroFollower;
        private readonly IPathShooting _heroShooting;

        private IObstacle _obstacle;

        public ObstacleOvercomingState(
            IPathStateMachine stateMachine,
            IPathFollower heroFollower,
            IPathShooting heroShooting)
        {
            _stateMachine = stateMachine;
            _heroFollower = heroFollower;
            _heroShooting = heroShooting;
        }

        public void Enter(IObstacle obstacle)
        {
            if (obstacle == null || obstacle.IsDestroyed)
            {
                OnObstacleOvercame();
                return;
            }

            _heroShooting.Enable();

            _obstacle = obstacle;
            _obstacle.Init();

            _heroFollower.SetLookTarget(_obstacle.LookAtPoint);

            _obstacle.Overcame += OnObstacleOvercame;
        }

        public void Exit() => _heroShooting.Disable();

        private void OnObstacleOvercame()
        {
            if (_obstacle != null)
                _obstacle.Overcame -= OnObstacleOvercame;

            _stateMachine.Enter<WaitForTapState>();
        }
    }
}