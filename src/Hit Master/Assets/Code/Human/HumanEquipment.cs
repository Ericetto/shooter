using UnityEngine;
using Code.Human.Mediator;
using Code.Weapon;

namespace Code.Human
{
    internal class HumanEquipment : HumanComponent
    {
        [SerializeField] protected Transform _rifleHolder;
        [SerializeField] protected Transform _pistolHolder;

        private readonly Vector3 _dropWeaponForce = new Vector3(0, 350, 0);

        public IGun Gun { get; protected set; }

        public void EquipGun(IGun gun)
        {
            Gun = gun;
            Gun.SetParent(gun.IsPistol ? _pistolHolder : _rifleHolder);
            ResetGunTransform();

            Mediator.SetGunType(gun.IsPistol);
            Mediator.AnimatorStateExited += ResetGunTransform;
        }

        public void DropGun()
        {
            Gun.SetParent(null);
            Gun.Collider.enabled = true;
            Gun.SetActivePhysics(true);
            Gun.Rigidbody.AddForce(_dropWeaponForce, ForceMode.Acceleration);
            Gun.Rigidbody.AddTorque(_dropWeaponForce);
        }

        private void ResetGunTransform()
        {
            if (Gun != null)
                Gun.ResetTransform();
        }

        private void OnDestroy() => 
            Mediator.AnimatorStateExited -= ResetGunTransform;
    }
}