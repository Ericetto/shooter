using UnityEngine;
using System.Collections;
using Code.Infrastructure;
using Code.Infrastructure.Services.Input;
using Code.Level.Way.Follower;

namespace Code.Level.Way.StateMachine.States
{
    public class WaitForTapState : IWayState
    {
        private readonly IWayStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IWayShooting _heroShooting;
        private readonly IInputService _inputService;

        public WaitForTapState(
            IWayStateMachine stateMachine,
            ICoroutineRunner coroutineRunner,
            IWayShooting heroShooting,
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

        public void Exit()
        {

        }

        private IEnumerator WaitForTapToMoveNext()
        {
            while (!_inputService.IsShootButtonDown())
                yield return null;

            yield return new WaitForSeconds(0.5f);

            _stateMachine.Enter<RunToNexPointState>();
        }
    }
}