using UnityEngine;
using Code.Extensions;
using Code.Human;
using Code.Logic.AnimatorState;
using Code.Weapon;

[RequireComponent(typeof(HumanAnimator))]
public class HumanEquipment : MonoBehaviour
{
    [SerializeField] protected Transform _rifleHolder;
    [SerializeField] protected Transform _pistolHolder;

    protected HumanAnimator _animator;

    public Gun Gun { get; protected set; }

    private void Awake()
    {
        _animator = GetComponent<HumanAnimator>();
        _animator.StateExited += OnAnimationChange;
    }

    public void EquipGun(Gun gun)
    {
        Gun = gun;
        Gun.transform.SetParent(gun.IsPistol ? _pistolHolder : _rifleHolder);
        ResetGunTransform();

        _animator.SetGunType(gun.IsPistol);
        _animator.StateExited += OnAnimationChange;
    }

    public void DropGun()
    {
        Gun.transform.SetParent(null);
        Gun.GetComponent<Collider>().enabled = true;
        Gun.SetActivePhysics(true);
        Gun.AddForce(new Vector3(0, 350, 0));
    }

    private void OnAnimationChange(AnimatorState _) => ResetGunTransform();

    private void ResetGunTransform()
    {
        if (Gun != null)
            Gun.transform.Reset();
    }

    private void OnDestroy() => _animator.StateExited -= OnAnimationChange;
}