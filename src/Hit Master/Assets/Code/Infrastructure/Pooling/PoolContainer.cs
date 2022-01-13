using UnityEngine;
using System.Collections.Generic;
using Code.Infrastructure.Services.AssetProvider;

namespace Code.Infrastructure.Pooling
{
    public class PoolContainer : IPoolContainer
    {
        private readonly IAssetProvider _assetProvider;

        private readonly string _prefabPath;
        private readonly Stack<PoolObject> _store = new Stack<PoolObject>(64);

        public PoolContainer(IAssetProvider assetProvider, string prefabPath)
        {
            _assetProvider = assetProvider;
            _prefabPath = prefabPath;
        }

        public PoolObject Get()
        {
            PoolObject obj;

            if (_store.Count > 0)
            {
                obj = _store.Pop();
            }
            else
            {
                GameObject go = _assetProvider.Instantiate(_prefabPath);
                obj = go.AddComponent<PoolObject>();
                obj.SetPool(this);
            }

            obj.SetActive(false);

            return obj;
        }

        public void Recycle(PoolObject obj)
        {
            if (obj != null && obj.Pool == this)
            {
                obj.SetActive(false);

                if (!_store.Contains(obj))
                    _store.Push(obj);
            }
            else
            {
                Debug.LogWarning("Invalid obj to recycle", obj);
            }
        }
    }
}