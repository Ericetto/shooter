using System;
using Code.Human;
using Code.Level.Way.StateMachine;
using Code.Level.Way.StateMachine.States;

namespace Code.Level
{
    public class Level : ILevel
    {
        private readonly HumanDeath _heroDeath;
        private readonly IWayStateMachine _wayStateMachine;
        
        public event Action Finished;
        public event Action Failed;

        public Level(HumanDeath heroDeath, IWayStateMachine wayStateMachine)
        {
            _heroDeath = heroDeath;
            _wayStateMachine = wayStateMachine;
        }

        public void Start()
        {
            _heroDeath.Happened += OnFailed;
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
            _heroDeath.Happened -= OnFailed;
            _wayStateMachine.Completed -= OnFinish;
        }
    }
}