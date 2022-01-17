using UnityEngine;

namespace Code.Weapon
{
    public interface IEquippable
    {
        void SetParent(Transform parent);
        void ResetTransform();
    }
}