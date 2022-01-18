using System;
using Code.Infrastructure.StateMachine;

namespace Code.Level.Way.StateMachine
{
    internal interface IWayStateMachine : IStateMachine
    {
        event Action Completed;
        void OnCompleted();
    }
}