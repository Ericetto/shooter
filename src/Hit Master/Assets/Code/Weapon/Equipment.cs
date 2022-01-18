using UnityEngine;
using Extensions;

namespace Code.Weapon
{
    internal class Equipment : MonoBehaviour
    {
        public void SetParent(Transform parent) => transform.SetParent(parent);
        public void ResetTransform() => transform.Reset();
    }
}