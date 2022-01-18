using Code.Infrastructure.StaticData;

namespace Code.Weapon.TriggerMechanism
{
    internal class SemiTriggerMechanism : Equipment, ITriggerMechanism
    {
        private bool _isPulledDown;

        public void Construct(WeaponData data) { }

        public bool PullTrigger()
        {
            if (_isPulledDown)
                return false;

            _isPulledDown = true;
            return true;
        }

        public void PullUpTrigger() => _isPulledDown = false;
    }
}