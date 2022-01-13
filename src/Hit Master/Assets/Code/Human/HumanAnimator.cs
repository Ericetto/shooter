using UnityEngine;
using System;
using Code.Logic.AnimatorState;

namespace Code.Human
{
    public class HumanAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _runBoolParameterHash = Animator.StringToHash("Run");
        private readonly int _shootingBoolParameterHash = Animator.StringToHash("Shooting");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _shootingStateHash = Animator.StringToHash("Shooting");

        private bool _canPlayShooting;

        public bool IsInTransition => _animator.IsInTransition(0);

        public AnimatorState State { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        public void SetActive(bool value) => _animator.enabled = value;

        public void Run() => _animator.SetBool(_runBoolParameterHash, true);

        public void Stop() => _animator.SetBool(_runBoolParameterHash, false);

        public void Shooting()
        {
            if (State != AnimatorState.Shooting)
            {
                _animator.SetBool(_shootingBoolParameterHash, true);
            }
            else if (_canPlayShooting)
            {
                _canPlayShooting = false;
                _animator.Play(_shootingStateHash, 0, 0);
            }
        }

        // Call from Gun Shooting animation clip
        private void ShootingClipFinished() => _canPlayShooting = true;

        public void StopShooting() => _animator.SetBool(_shootingBoolParameterHash, false);

        private AnimatorState StateFor(int stateHash)
        {
            if (stateHash == _idleStateHash)
                return AnimatorState.Idle;
            if (stateHash == _shootingStateHash)
                return AnimatorState.Shooting;
            if (stateHash == _runStateHash)
                return AnimatorState.Run;

            return AnimatorState.Unknown;
        }
    }
}