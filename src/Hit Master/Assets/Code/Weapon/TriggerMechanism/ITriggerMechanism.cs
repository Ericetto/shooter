using Code.Infrastructure.StaticData;

namespace Code.Weapon.TriggerMechanism
{
    internal interface ITriggerMechanism : IEquippable
    {
        void Construct(WeaponData data);
        bool PullTrigger();
        void PullUpTrigger();
    }
}