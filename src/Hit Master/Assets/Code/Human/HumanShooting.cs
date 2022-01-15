using UnityEngine;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator), typeof(HumanEquipment))]
    public abstract class HumanShooting : MonoBehaviour
    {
        [SerializeField] protected HumanAnimator _animator;
        [SerializeField] protected HumanEquipment _equipment;
        
        protected bool _isActive;

        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;
        
        protected virtual void PullTrigger()
        {
            if (!_isActive || _animator.IsInTransition)
                return;

            _animator.Shoot();
            _equipment.Gun.PullTrigger();
        }

        protected virtual void PullUpTrigger() => _equipment.Gun.PullUpTrigger();
        
        private void PullTriggerByAnimation() => PullTrigger();
    }
}