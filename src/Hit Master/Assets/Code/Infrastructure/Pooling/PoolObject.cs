using UnityEngine;

namespace Code.Infrastructure.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public PoolContainer Pool { get; private set; }

        public Transform CachedTransform { get; private set; }

        private void Awake()
        {
            CachedTransform = transform;
        }

        public void SetPool(PoolContainer pool)
        {
            Pool = pool;
        }

        public void Recycle()
        {
            if (Pool != null)
                Pool.Recycle(this);
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}