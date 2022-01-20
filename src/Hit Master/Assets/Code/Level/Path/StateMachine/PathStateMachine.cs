using System;
using System.Collections.Generic;
using Code.Infrastructure;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.StateMachine;
using Code.Level.Path.Follower;
using Code.Level.Path.StateMachine.States;

namespace Code.Level.Path.StateMachine
{
    public class PathStateMachine : StateMachineBase, IPathStateMachine
    {
        public event Action Completed;

        public PathStateMachine(
            IWayPoint[] points,
            IPathFollower heroFollower,
            IPathShooting heroShooting,
            ICoroutineRunner coroutineRunner,
            IInputService inputService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(WaitForTapState)] = new WaitForTapState(
                    this, coroutineRunner, heroShooting, inputService),

                [typeof(RunToNexPointState)] = new RunToNexPointState(
                    this, heroFollower, heroShooting, points),

                [typeof(ObstacleOvercomingState)] = new ObstacleOvercomingState(
                    this, heroFollower, heroShooting)
            };
        }

        public virtual void OnCompleted() => Completed?.Invoke();
    }
}