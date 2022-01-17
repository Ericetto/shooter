using Code.Human;
using Code.Level.Way.StateMachine;
using Code.Level.Way.StateMachine.States;
using System;

namespace Code.Level
{
    public class Level : ILevel
    {
        private readonly HumanHealth _heroMediator;
        private readonly IWayStateMachine _wayStateMachine;
        
        public event Action Finished;
        public event Action Failed;

        public Level(HumanHealth heroHealth, IWayStateMachine wayStateMachine)
        {
            _heroMediator = heroHealth;
            _wayStateMachine = wayStateMachine;
        }

        public void Start()
        {
            _heroMediator.Died += OnFailed;
            _wayStateMachine.Completed += OnFinish;

            _wayStateMachine.Enter<RunToNexPointState>();
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
            _wayStateMachine.Completed -= OnFinish;
        }
    }
}