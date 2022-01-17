using Code.Infrastructure.Physics;
using Code.Infrastructure.Pooling;
using Code.Infrastructure.StaticData;
using Code.Weapon.TriggerMechanism;
using UnityEngine;

namespace Code.Weapon
{
    public interface IGun : IEquippable, IPhysical
    {
        bool IsPistol { get; }
        void Construct(WeaponData data, ITriggerMechanism triggerMechanism, IPoolContainer bulletPool);
        bool PullTrigger();
        void PullUpTrigger();
        void LookAt(Vector3 at);
    }
}