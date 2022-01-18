using UnityEngine;

namespace Code.Weapon
{
    internal interface IEquippable
    {
        void SetParent(Transform parent);
        void ResetTransform();
    }
}