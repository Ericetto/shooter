using UnityEngine;
using Code.Weapon;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator))]
    public abstract class HumanAttack : MonoBehaviour
    {
        [SerializeField] protected HumanAnimator _animator;
        [SerializeField] protected Gun _gun;
        
        protected bool _isActive;

        public void Enable() => _isActive = true;

        public void Disable() => _isActive = false;

        protected virtual void GunShoot() { }
    }
}