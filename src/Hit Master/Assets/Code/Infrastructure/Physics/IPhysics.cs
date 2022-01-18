using UnityEngine;

namespace Code.Infrastructure.Physics
{
    public interface IPhysical
    {
        Rigidbody Rigidbody { get; }
        Collider Collider { get; }
        void SetActivePhysics(bool value);
    }
}