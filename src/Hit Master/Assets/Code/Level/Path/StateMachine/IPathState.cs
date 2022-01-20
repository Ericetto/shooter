using Code.Infrastructure.StateMachine;

namespace Code.Level.Path.StateMachine
{
    public interface IPathState : IState
    {

    }

    public interface IPathPayloadedState<TPayload> : IPayloadedState<TPayload>
    {

    }
}