using UnityEngine;
using Code.Infrastructure.StaticData;

namespace Code.Weapon.TriggerMechanism
{
    public class AutomaticTriggerMechanism : MonoBehaviour, ITriggerMechanism
    {
        private float _betweenShotsTime;
        private float _cooldown;

        public void Construct(WeaponData data)
        {
            _betweenShotsTime =  60f / data.FireRate;
        }

        public void Update()
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;
        }

        public bool PullTrigger()
        {
            if (_cooldown > 0)
                return false;

            _cooldown = _betweenShotsTime;
            return true;
        }

        public void PullUpTrigger() { }
    }
}