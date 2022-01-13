using UnityEngine;

namespace Code.Infrastructure.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Load(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Load(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            GameObject prefab = Load(path);
            return Object.Instantiate(prefab, parent);
        }

        private GameObject Load(string path) => Resources.Load<GameObject>(path);
    }
}