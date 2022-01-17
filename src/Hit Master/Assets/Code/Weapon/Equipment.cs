using UnityEngine;
using Code.Extensions;

namespace Code.Weapon
{
    public class Equipment : MonoBehaviour
    {
        public void SetParent(Transform parent) => transform.SetParent(parent);
        public void ResetTransform() => transform.Reset();
    }
}