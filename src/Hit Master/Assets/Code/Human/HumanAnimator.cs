using UnityEngine;
using System;
using Code.AnimatorState;
using Code.Human.Mediator;

namespace Code.Human
{
    [RequireComponent(typeof(Animator))]
    public class HumanAnimator : HumanComponent, IAnimationStateReader
    {
        private readonly int _pistolTriggerParameterHash = Animator.StringToHash("Pistol");
        private readonly int _rifleTriggerParameterHash = Animator.StringToHash("Rifle");

        private readonly int _runBoolParameterHash = Animator.StringToHash("Run");
        private readonly int _shootingBoolParameterHash = Animator.StringToHash("Shooting");

        private readonly int _emptyStateHash = Animator.StringToHash("Empty");

        private int _idleStateHash;
        private int _runStateHash;
        private int _shootingStateHash;
        
        private Animator _animator;

        public bool IsInTransition => _animator.IsInTransition(0);

        public AnimatorState.AnimatorState State { get; private set; }

        public event Action<AnimatorState.AnimatorState> StateEntered;
        public event Action<AnimatorState.AnimatorState> StateExited;

        protected override void OnAwake() => _animator = GetComponent<Animator>();

        public void SetGunType(bool isPistol)
        {
            string gunType = isPistol ? "Pistol" : "Rifle";

            _idleStateHash = Animator.StringToHash($"{gunType} Idle");
            _runStateHash = Animator.StringToHash($"{gunType} Run");
            _shootingStateHash = Animator.StringToHash($"{gunType} Shooting");
            
            _animator.SetTrigger(isPistol ? _pistolTriggerParameterHash : _rifleTriggerParameterHash);
            _animator.Play(_emptyStateHash);
        }

        public void SetActive(bool value) => _animator.enabled = value;
        public void Run() => _animator.SetBool(_runBoolParameterHash, true);
        public void Stop() => _animator.SetBool(_runBoolParameterHash, false);
        public void StartShooting() => _animator.SetBool(_shootingBoolParameterHash, true);
        public void StopShooting() => _animator.SetBool(_shootingBoolParameterHash, false);

        public void Shoot()
        {
            if (State != AnimatorState.AnimatorState.Shooting)
                StartShooting();
            else
                _animator.Play(_shootingStateHash, 0, 0);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState.AnimatorState StateFor(int stateHash)
        {
            if (stateHash == _idleStateHash)
                return AnimatorState.AnimatorState.Idle;
            if (stateHash == _shootingStateHash)
                return AnimatorState.AnimatorState.Shooting;
            if (stateHash == _runStateHash)
                return AnimatorState.AnimatorState.Run;

            return AnimatorState.AnimatorState.Unknown;
        }
    }
}