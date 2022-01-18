using Code.Level;

namespace Code.Infrastructure.StateMachine.States
{
    internal class GameLoopState : IPayloadedState<ILevel>
    {
        private readonly IStateMachine _stateMachine;
        private ILevel _level;

        public GameLoopState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(ILevel level)
        {
            _level = level;

            _level.Finished += RestartLevel;
            _level.Failed += RestartLevel;

            _level.Start();
        }

        public void Exit()
        {
            _level.Finished -= RestartLevel;
            _level.Failed -= RestartLevel;
        }

        private void RestartLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
    }
}