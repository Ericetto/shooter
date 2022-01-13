using UnityEngine;

namespace Code.Infrastructure.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public PoolContainer Pool { get; private set; }

        public void SetPool(PoolContainer pool) => Pool = pool;

        public void Recycle()
        {
            if (Pool == null)
                return;

            Pool.Recycle(this);
            OnRecycle();
        }

        protected virtual void OnRecycle() { }

        public void SetActive(bool state) => gameObject.SetActive(state);
    }
}