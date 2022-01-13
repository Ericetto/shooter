namespace Code.Logic.AnimatorState
{
    public interface IAnimationStateReader
    {
        AnimatorState State { get; }
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}