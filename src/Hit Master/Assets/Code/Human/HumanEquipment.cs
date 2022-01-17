using UnityEngine;
using Code.Human;
using Code.Logic.AnimatorState;
using Code.Weapon;

[RequireComponent(typeof(HumanAnimator))]
public class HumanEquipment : MonoBehaviour
{
    [SerializeField] protected Transform _rifleHolder;
    [SerializeField] protected Transform _pistolHolder;

    protected HumanAnimator _animator;

    public IGun Gun { get; protected set; }

    private void Awake()
    {
        _animator = GetComponent<HumanAnimator>();
        _animator.StateExited += OnAnimationChanged;
    }

    public void EquipGun(IGun gun)
    {
        Gun = gun;
        Gun.SetParent(gun.IsPistol ? _pistolHolder : _rifleHolder);
        ResetGunTransform();

        _animator.SetGunType(gun.IsPistol);
        _animator.StateExited += OnAnimationChanged;
    }

    public void DropGun()
    {
        Gun.SetParent(null);
        Gun.Collider.enabled = true;
        Gun.SetActivePhysics(true);
        Gun.AddForce(new Vector3(0, 350, 0));
    }

    private void OnAnimationChanged(AnimatorState _) => ResetGunTransform();

    private void ResetGunTransform()
    {
        if (Gun != null)
            Gun.ResetTransform();
    }

    private void OnDestroy() => _animator.StateExited -= OnAnimationChanged;
}