using UnityEngine;

namespace AssetProvider
{
    public class ResourceProvider : IAssetProvider
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

        public GameObject Load(string path) => Resources.Load<GameObject>(path);
    }
}