using UnityEngine;
using Code.Infrastructure.Physics;
using Code.Infrastructure.StaticData;
using Code.Weapon.TriggerMechanism;
using Pooling;

namespace Code.Weapon
{
    internal interface IGun : IEquippable, IPhysical
    {
        bool IsPistol { get; }
        void Construct(WeaponData data, ITriggerMechanism triggerMechanism, IPoolContainer bulletPool);
        bool PullTrigger();
        void PullUpTrigger();
        void LookAt(Vector3 at);
    }
}