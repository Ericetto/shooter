using UnityEngine;
using Code.Logic.AnimatorState;
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

        protected virtual void Shoot()
        {
            if (_animator.State == AnimatorState.Shooting)
                _animator.Shoot();
            else
                GunShoot();
        }
        
        protected virtual void GunShoot()
        {
            if (!_animator.IsInTransition)
                Gun.Shoot();
        }

        private void GunShootByAnimation() => GunShoot();
    }
}