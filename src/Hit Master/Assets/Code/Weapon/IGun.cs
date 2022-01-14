using Code.Infrastructure.Pooling;
using Code.Infrastructure.StaticData;
using Code.Weapon.TriggerMechanism;

namespace Code.Weapon
{
    public interface IGun
    {
        float Damage { get; }
        bool IsPistol { get; }
        void Construct(WeaponData data, ITriggerMechanism triggerMechanism, IPoolContainer bulletPool);
        bool PullTrigger();
        void PullUpTrigger();
    }
}