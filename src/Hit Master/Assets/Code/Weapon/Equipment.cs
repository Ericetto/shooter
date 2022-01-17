using UnityEngine;
using Code.Extensions;

public class Equipment : MonoBehaviour
{
    public void SetParent(Transform parent) => transform.SetParent(parent);
    public void ResetTransform() => transform.Reset();
}