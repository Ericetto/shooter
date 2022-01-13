using Code.Infrastructure.StaticData;
using UnityEngine;

public abstract class WeaponBase: MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    
    public float Damage { get; protected set; }

    private void Start() => SetActivePhysics(false);

    public void Construct(WeaponData data)
    {
        Damage = data.Damage;
    }

    public abstract void Attack();

    public void AddForce(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Acceleration);
        _rigidbody.AddTorque(force);
    }

    public void SetActivePhysics(bool value)
    {
        _rigidbody.useGravity = value;
        _rigidbody.isKinematic = !value;
    }
}