using UnityEngine;
using Code.Weapon;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator))]
    public abstract class HumanShooting : MonoBehaviour
    {
        [SerializeField] protected HumanEquipment _equipment;
        [SerializeField] protected HumanAnimator _animator;
        
        protected Gun Gun => _equipment.Gun;

        protected bool _isActive;

        public void Enable() => _isActive = true;
        public void Disable() => _isActive = false;

        protected virtual void Shoot() => _animator.Shoot();

        // Calls from "Gun Shooting" animation clip
        protected virtual void GunShootByAnimation()
        {
            if (!_animator.IsInTransition)
                Gun.Shoot();
        }
    }
}