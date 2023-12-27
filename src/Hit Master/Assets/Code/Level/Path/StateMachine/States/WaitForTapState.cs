using System.Collections;
using Code.Infrastructure;
using Code.Infrastructure.Services.Input;
using Code.Level.Path.Follower;
using UnityEngine;

namespace Code.Level.Path.StateMachine.States
{
    public class WaitForTapState : IPathState
    {
        private readonly IPathStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPathShooting _heroShooting;
        private readonly IInputService _inputService;

        public WaitForTapState(
            IPathStateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            IPathShooting heroShooting,
            IInputService inputService)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _heroShooting = heroShooting;
            _inputService = inputService;
        }

        public void Enter()
        {
            _heroShooting.Disable();

            _coroutineRunner.StartCoroutine(
                WaitForTapToMoveNext());
        }

        public void Exit() { }

        private IEnumerator WaitForTapToMoveNext()
        {
            while (!_inputService.IsShootButtonDown())
                yield return null;

            yield return new WaitForSeconds(0.5f);

            _stateMachine.Enter<RunToNexPointState>();
        }
    }
}