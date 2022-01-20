using Code.Human;
using System;
using Code.Level.Path.StateMachine;
using Code.Level.Path.StateMachine.States;

namespace Code.Level
{
    public class Level : ILevel
    {
        private readonly HumanHealth _heroMediator;
        private readonly IPathStateMachine _pathStateMachine;
        
        public event Action Finished;
        public event Action Failed;

        public Level(HumanHealth heroHealth, IPathStateMachine pathStateMachine)
        {
            _heroMediator = heroHealth;
            _pathStateMachine = pathStateMachine;
        }

        public void Start()
        {
            _heroMediator.Died += OnFailed;
            _pathStateMachine.Completed += OnFinish;

            _pathStateMachine.Enter<RunToNexPointState>();
        }

        public void OnFinish()
        {
            Finished?.Invoke();
            Cleanup();
        }

        public void OnFailed()
        {
            Failed?.Invoke();
            Cleanup();
        }

        private void Cleanup()
        {
            _heroMediator.Died -= OnFailed;
            _pathStateMachine.Completed -= OnFinish;
        }
    }
}