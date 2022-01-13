using UnityEngine;
using System.Collections.Generic;
using Code.Infrastructure.Services.AssetProvider;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.Infrastructure.Pooling
{
    public class PoolContainer : IPoolContainer
    {
        private readonly IAssetProvider _assetProvider;

        private readonly string _prefabPath;
        private readonly Stack<PoolObject> _store = new Stack<PoolObject>(64);

        private GameObject _prefab;

        public PoolContainer(IAssetProvider assetProvider, string prefabPath)
        {
            _assetProvider = assetProvider;
            _prefabPath = prefabPath;
        }

        public PoolObject Get()
        {
            if (_prefab == null)
                if (!LoadPrefab())
                    return null;

            PoolObject obj;

            if (_store.Count > 0)
            {
                obj = _store.Pop();
            }
            else
            {
                var go = _assetProvider.Instantiate(_prefabPath);
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
#if UNITY_EDITOR
                Debug.LogWarning("Invalid obj to recycle", obj);
#endif
            }
        }

        private bool LoadPrefab()
        {
            _prefab = Resources.Load<GameObject>(_prefabPath);

            if (_prefab == null)
            {
                Debug.LogWarning("Cant load asset " + _prefabPath);
                return false;
            }

#if UNITY_EDITOR
            if (_prefab.GetComponent<PoolObject>() != null)
            {
                Debug.LogWarning("PoolObject cant be used on prefabs");

                _prefab = null;

                EditorApplication.isPaused = true;

                return false;
            }
#endif
            return true;
        }
    }
}