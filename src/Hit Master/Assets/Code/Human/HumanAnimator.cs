using UnityEngine;
using System;
using Code.Logic.AnimatorState;

namespace Code.Human
{
    public class HumanAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _pistolTriggerParameterHash = Animator.StringToHash("Pistol");
        private readonly int _rifleTriggerParameterHash = Animator.StringToHash("Rifle");

        private readonly int _runBoolParameterHash = Animator.StringToHash("Run");
        private readonly int _shootingBoolParameterHash = Animator.StringToHash("Shooting");

        private readonly int _emptyStateHash = Animator.StringToHash("Empty");

        private int _idleStateHash;
        private int _runStateHash;
        private int _shootingStateHash;

        private bool _canPlayShooting;

        public bool IsInTransition => _animator.IsInTransition(0);

        public AnimatorState State { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void SetGunType(bool isPistol)
        {
            string gunType = isPistol ? "Pistol" : "Rifle";

            _idleStateHash = Animator.StringToHash($"{gunType} Idle");
            _runStateHash = Animator.StringToHash($"{gunType} Run");
            _shootingStateHash = Animator.StringToHash($"{gunType} Shooting");
            
            _animator.SetTrigger(isPistol ? _pistolTriggerParameterHash : _rifleTriggerParameterHash);

            _animator.Play(_emptyStateHash);
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
        
        public void Run() => _animator.SetBool(_runBoolParameterHash, true);
        public void Stop() => _animator.SetBool(_runBoolParameterHash, false);
        
        public void StartShooting() => _animator.SetBool(_shootingBoolParameterHash, true);

        public void Shoot()
        {
            if (State != AnimatorState.Shooting)
            {
                StartShooting();
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

        public void SetActive(bool value) => _animator.enabled = value;

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