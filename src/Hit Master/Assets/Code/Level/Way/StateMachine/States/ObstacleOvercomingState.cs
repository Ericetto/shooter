using Code.Level.Obstacle;
using Code.Level.Way.Follower;

namespace Code.Level.Way.StateMachine.States
{
    public class ObstacleOvercomingState : IWayPayloadedState<IObstacle>
    {
        private readonly IWayStateMachine _stateMachine;
        private readonly IWayFollower _heroFollower;
        private readonly IWayShooting _heroShooting;

        private IObstacle _obstacle;

        public ObstacleOvercomingState(
            IWayStateMachine stateMachine,
            IWayFollower heroFollower,
            IWayShooting heroShooting)
        {
            _stateMachine = stateMachine;
            _heroFollower = heroFollower;
            _heroShooting = heroShooting;
        }

        public void Enter(IObstacle obstacle)
        {
            if (obstacle == null)
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

        public void Exit()
        {
            _heroShooting.Disable();
        }

        private void OnObstacleOvercame()
        {
            if (_obstacle != null)
                _obstacle.Overcame -= OnObstacleOvercame;

            _stateMachine.Enter<WaitForTapState>();
        }
    }
}