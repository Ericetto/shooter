using UnityEngine;

namespace Code.Infrastructure.Physics
{
    public interface IPhysical
    {
        Collider Collider { get; }
        void AddForce(Vector3 force);
        void SetActivePhysics(bool value);
    }
}