using UnityEngine;
using Code.Extensions;

namespace Code.Infrastructure.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public PoolContainer Pool { get; private set; }

        public void SetPool(PoolContainer pool) => Pool = pool;

        public void Recycle()
        {
            if (Pool != null)
                Pool.Recycle(this);
        }

        public GameObject SetActive(bool state)
        {
            gameObject.SetActive(state);
            return gameObject;
        }

        public GameObject SetTransform(Transform other)
        {
            transform.CopyFrom(other);
            return gameObject;
        }
    }
}