using Code.Infrastructure.StateMachine;

namespace Code.Level.Way.StateMachine
{
    public interface IWayState : IState
    {

    }

    public interface IWayPayloadedState<TPayload> : IPayloadedState<TPayload>
    {

    }
}