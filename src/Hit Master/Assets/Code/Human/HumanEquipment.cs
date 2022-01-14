using UnityEngine;
using Code.Human;
using Code.Weapon;

public class HumanEquipment : MonoBehaviour
{
    [SerializeField] protected HumanAnimator _animator;
    [SerializeField] protected Transform _rifleHolder;
    [SerializeField] protected Transform _pistolHolder;

    public Gun Gun { get; protected set; }

    public void EquipGun(Gun gun)
    {
        Gun = gun;
        Gun.transform.SetParent(gun.IsPistol ? _pistolHolder : _rifleHolder);
        Gun.transform.localPosition = Vector3.zero;
        Gun.transform.localRotation = Quaternion.identity;
        Gun.transform.localScale = Vector3.one;

        _animator.SetGunType(gun.IsPistol);
    }

    public void DropGun()
    {
        Gun.transform.SetParent(null);
        Gun.GetComponent<Collider>().enabled = true;
        Gun.SetActivePhysics(true);
        Gun.AddForce(new Vector3(0, 350, 0));
    }
}