using Code.Level.Way;
using Code.Level.Way.Follower;
using Code.Level.Way.StateMachine;

namespace Code.Infrastructure.Factory
{
    internal interface IWayFactory
    {
        IWayStateMachine CreateWayStateMachine(
            IWayPoint[] wayPoints,
            IWayFollower wayFollower,
            IWayShooting wayShooting);
    }
}