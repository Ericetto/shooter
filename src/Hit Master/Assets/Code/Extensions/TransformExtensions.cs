﻿using UnityEngine;

namespace Code.Extensions
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
    }
}