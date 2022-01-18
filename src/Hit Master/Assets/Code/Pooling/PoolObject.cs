using UnityEngine;

namespace Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public IPoolContainer Pool { get; private set; }

        public void SetPool(IPoolContainer pool) => Pool = pool;

        public void Recycle()
        {
            if (Pool != null)
                Pool.Recycle(this);
        }

        public void SetActive(bool state) => gameObject.SetActive(state);
    }
}