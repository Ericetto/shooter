using UnityEngine;

namespace Code.AnimatorState
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        private IAnimationStateReader _stateReader;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            FindReader(animator).EnteredState(stateInfo.shortNameHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            FindReader(animator).ExitedState(stateInfo.shortNameHash);
        }

        private IAnimationStateReader FindReader(Animator animator)
        {
            if (_stateReader != null)
                return _stateReader;

            _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
            return _stateReader;
        }
    }
}