using Code.Infrastructure.StaticData;
using UnityEngine;

namespace Code.Weapon.TriggerMechanism
{
    public class SemiTriggerMechanism : MonoBehaviour, ITriggerMechanism
    {
        private bool _pullDown;

        public void Construct(WeaponData data) { }

        public bool PullTrigger()
        {
            if (_pullDown)
                return false;

            _pullDown = true;
            return true;
        }

        public void PullUpTrigger() => _pullDown = false;
    }
}