using UnityEngine;

namespace Code.Human
{
    [RequireComponent(typeof(HumanAnimator))]
    public abstract class HumanAttack : MonoBehaviour
    {
        [SerializeField] protected HumanAnimator _animator;
        [SerializeField] protected Transform _weaponHolder;

        protected WeaponBase _weapon;
        protected bool _isActive;

        public virtual void EquipWeapon(WeaponBase weapon)
        {
            _weapon = weapon;
            _weapon.transform.SetParent(_weaponHolder);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
            _weapon.transform.localScale = Vector3.one;
        }

        public void Enable() => _isActive = true;

        public void Disable() => _isActive = false;

        // Calls from "Gun Shooting" animation clip
        protected virtual void WeaponAttackByAnimation() { }

        public virtual void DropWeapon()
        {
            _weapon.transform.SetParent(null);
            _weapon.GetComponent<Collider>().enabled = true;
            _weapon.SetActivePhysics(true);
            _weapon.AddForce(new Vector3(0, 500, 0));
        }
    }
}