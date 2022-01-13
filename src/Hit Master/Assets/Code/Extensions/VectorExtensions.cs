using UnityEngine;

namespace Code.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 AddX(this Vector3 vector, float x) =>
            new Vector3(vector.x + x, vector.y, vector.z);

        public static Vector3 AddY(this Vector3 vector, float y) =>
            new Vector3(vector.x, vector.y + y, vector.z);

        public static Vector3 WithX(this Vector3 vector, float x) =>
            new Vector3(x, vector.y, vector.z);

        public static Vector3 WithY(this Vector3 vector, float y) =>
            new Vector3(vector.x, y, vector.z);
    }
}