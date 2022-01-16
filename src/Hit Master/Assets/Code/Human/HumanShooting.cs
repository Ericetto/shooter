using UnityEngine;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator), typeof(HumanEquipment))]
    public abstract class HumanShooting : MonoBehaviour
    {
        [SerializeField] protected HumanAnimator _animator;
        [SerializeField] protected HumanEquipment _equipment;
        
        private bool _isActive;

        public bool IsEnable => _isActive;
        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;

        private bool CanPullTrigger => _isActive && !_animator.IsInTransition;

        protected void PullTrigger()
        {
            if (CanPullTrigger && _equipment.Gun.PullTrigger())
                _animator.Shoot();
        }

        protected void PullUpTrigger() => _equipment.Gun.PullUpTrigger();
        
        private void PullTriggerByAnimation()
        {
            if (CanPullTrigger)
                _equipment.Gun.PullTrigger();
        }
    }
}