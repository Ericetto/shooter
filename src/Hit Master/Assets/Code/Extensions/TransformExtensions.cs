using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }

        public static void CopyFrom(this Transform transform, Transform other, bool withScale = false)
        {
            transform.localPosition = other.localPosition;
            transform.localRotation = other.localRotation;

            if (withScale)
                transform.localScale = other.localScale;
        }
    }
}