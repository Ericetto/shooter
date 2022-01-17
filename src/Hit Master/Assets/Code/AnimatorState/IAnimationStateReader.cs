namespace Code.AnimatorState
{
    public interface IAnimationStateReader
    {
        AnimatorState State { get; }
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
    }
}