using System;
using Code.Infrastructure.StateMachine;

namespace Code.Level.Path.StateMachine
{
    public interface IPathStateMachine : IStateMachine
    {
        event Action Completed;
        void OnCompleted();
    }
}