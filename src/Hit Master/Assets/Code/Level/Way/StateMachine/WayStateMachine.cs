using System;
using System.Collections.Generic;
using Code.Infrastructure;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.StateMachine;
using Code.Level.Way.Follower;
using Code.Level.Way.StateMachine.States;

namespace Code.Level.Way.StateMachine
{
    public class WayStateMachine : StateMachineBase, IWayStateMachine
    {
        public event Action Completed;

        public WayStateMachine(
            IWayPoint[] points,
            IWayFollower heroFollower,
            IWayShooting heroShooting,
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