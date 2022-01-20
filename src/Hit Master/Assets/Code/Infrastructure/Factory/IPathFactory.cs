using Code.Level.Path;
using Code.Level.Path.Follower;
using Code.Level.Path.StateMachine;

namespace Code.Infrastructure.Factory
{
    public interface IPathFactory
    {
        IPathStateMachine CreateWayStateMachine(
            IWayPoint[] wayPoints,
            IPathFollower pathFollower,
            IPathShooting pathShooting);
    }
}