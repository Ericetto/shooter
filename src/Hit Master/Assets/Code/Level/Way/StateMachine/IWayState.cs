using Code.Infrastructure.StateMachine;

namespace Code.Level.Way.StateMachine
{
    internal interface IWayState : IState
    {

    }

    internal interface IWayPayloadedState<TPayload> : IPayloadedState<TPayload>
    {

    }
}